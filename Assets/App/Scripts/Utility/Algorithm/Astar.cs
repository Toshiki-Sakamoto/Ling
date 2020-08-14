//
// Astar.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.14
//

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Ling.Utility.Algorithm
{
	/// <summary>
	/// AStartAlgorithm
	/// </summary>
	public class Astar
    {
		#region 定数, class, enum

		public class Param
		{
			public int startX, startY;
			public int endX, endY;
			public int width;
			
			public System.Func<Vector2Int, bool> onCanMove;			// 移動可能か
			public System.Func<Vector2Int, int> onTileCostGetter;	// 指定座標の移動コストを取得する(必要であれば)
			public bool useDiagonal = true;	// 斜めを使用する
		}

		private class Node
		{
			public Node parent;	// 親ノード

			public int posX, posY;
			public int cost;	// 実コスト
			public int estimatedCost;	// 推定コスト
			public int score;	// スコア
		}

		#endregion


		#region public, protected 変数


		#endregion


		#region private 変数

		// Nodeはキャッシュして使い回す
		private Stack<Node> _usedNodes = new Stack<Node>();
		private Stack<Node> _unusedNodes = new Stack<Node>();

		private SortedDictionary<int, List<Node>> _openedNodes = new SortedDictionary<int, List<Node>>();
		private HashSet<int> _usedIndexes = new HashSet<int>();	// 使用済みの座標

		private Param _param;
		private Node _goalNode;

		#endregion


		#region プロパティ

		/// <summary>
		/// 正常に終了した場合true
		/// </summary>
		public bool IsSuccess { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
		}

		public bool Execute(Param param)
		{
			_param = param;
			IsSuccess = false;

			// そもそも開始地点が範囲内じゃない場合終了
			if (!param.onCanMove(new Vector2Int(_param.startX, _param.startY)))
			{
				Log.Error("開始地点が範囲外です");
				return false;
			}

			ResetNodeAll();

			_openedNodes.Clear();
			_usedIndexes.Clear();
			_goalNode = null;

			// Nodeを作成する
			var rootNode = CreateNode(_param.startX, _param.startY, null);
			CalcScore(rootNode, 0);

			// Nodeの作成に失敗したときは何もしない
			if (rootNode == null)
			{ 
				Utility.Log.Error("開始位置のNodeの作成に失敗しました");
				return false;
			}

			if (!ExecuteInternal(rootNode))
			{
				return false;
			}

			IsSuccess = true;
			return true;
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 実際のルートを見つける処理の実行
		/// </summary>
		/// <param name="node"></param>
		private bool ExecuteInternal(Node node)
		{
			var cost = node.cost;

			// 周りを開ける
			Utility.Map.CallDirection(node.posX, node.posY, 
				(posX_, posY_) =>
				{
					var childNode = CreateNode(posX_, posY_, node);

					// もしゴール地点なら終了！
					if (_param.endX == posX_ && _param.endY == posY_)
					{
						_goalNode = childNode;
						return true;
					}

					CalcScore(childNode, cost);

					return false;
				}, 
				useDiagonal: _param.useDiagonal);

			if (_goalNode != null)
			{
				return true;
			}

			// 一番スコアが低いNodeから走査していく
			var nodes = _openedNodes.FirstOrDefault().Value;
			if (nodes.IsNullOrEmpty())
			{
				Utility.Log.Error("Nodeが残っていない");
				return false;
			}

			// 走査前に先にCloseとしておく
			var rootNode = GetMinCostNode(nodes);
			var score = rootNode.score;

			nodes.Remove(rootNode);
				
			// Listが空になったら元から削除
			if (nodes.IsNullOrEmpty())
			{
				_openedNodes.Remove(score);
			}

			return ExecuteInternal(rootNode);
		}

		/// <summary>
		/// Nodeを作成する
		/// </summary>
		/// <param name="posX">Node X座標</param>
		/// <param name="posY">Node Y座標<</param>
		/// <param name="parent">親Node</param>
		/// <returns></returns>
		private Node CreateNode(int posX, int posY, Node parent)
		{
			// すでに一度通った場所は何もしない
			var index = posY * _param.width + posX;
			if (_usedIndexes.Contains(index)) return null;

			// 移動できない場合は何もしない
			if (!_param.onCanMove(new Vector2Int(posX, posY)))
			{ 
				// 一度通った場所としておくことで次回から判定されない
				_usedIndexes.Add(index);
				return null;
			}

			var node = PopNode();
			node.posX = posX;
			node.posY = posY;
			
			// 親ノードを設定しておく
			node.parent = parent;

			return node;
		}

		/// <summary>
		/// 指定Nodeのスコアを計算する
		/// </summary>
		/// <param name="node">計算対象Node</param>
		/// <param name="cost">開始地点からのコスト</param>
		private void CalcScore(Node node, int cost)
		{
			// 移動コストを取得
			var addCost = _param.onTileCostGetter?.Invoke(new Vector2Int(node.posX, node.posY)) ?? 1;
			node.cost = cost + addCost;

			// 推定コスト
			node.estimatedCost = CalcEstimatedCost(node.posX, node.posY);

			// スコア = 実コスト＋推定コスト
			node.score = node.cost + node.estimatedCost;

			// 使用済みリストの中に、ScoreをKeyとしてソートする
			if (!_openedNodes.TryGetValue(node.score, out var nodes))
			{
				nodes = new List<Node>();
				_openedNodes.Add(node.score, nodes);
			}

			nodes.Add(node);

			// Indexを保存する
			var index = node.posY * _param.width + node.posX;
			_usedIndexes.Add(index);
		}

		/// <summary>
		/// 推定コストを求める
		/// </summary>
		private int CalcEstimatedCost(int x, int y)
		{
			var dx = _param.endX - x;
			var dy = _param.endY - y;

			if (_param.useDiagonal)
			{
				// 斜めを許可している場合比較して高い値が推定コストとなる
				return Mathf.Max(dx, dy);
			}
			else
			{
				// 斜めを許可していない場合、推定コストはdx+dy
				return dx + dy;
			}
		}

		/// <summary>
		/// 最もコストが小さいNodeを取得する
		/// </summary>
		/// <param name="nodes"></param>
		/// <returns></returns>
		private Node GetMinCostNode(List<Node> nodes) =>
			nodes.OrderBy(node_ => node_.cost).FirstOrDefault();

		/// <summary>
		/// 未使用スタックからNodeを取得する
		/// もし未使用スタックがない場合は作成する
		/// </summary>
		private Node PopNode()
		{
			Node node = null;

			if (_unusedNodes.Count == 0)
			{
				node = new Node();
			}
			else
			{
				node = _unusedNodes.Pop();
			}
			
			_usedNodes.Push(node);

			return node;
		}

		/// <summary>
		/// Nodeをすべて未使用に戻す
		/// </summary>
		private void ResetNodeAll()
		{
			foreach (var node in _usedNodes)
			{
				_unusedNodes.Push(node);
			}

			_usedNodes.Clear();
		}

		#endregion
	}
}
