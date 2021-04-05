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
using Cysharp.Threading.Tasks;
using Ling.Utility.Extensions;

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
			public Vector2Int start;
			public Vector2Int end;
			public int width;

			public System.Func<Vector2Int, bool> onCanMove; // 移動可能か
			public System.Func<Vector2Int, bool> onCanDiagonalMove; // 斜め移動可能か
			public System.Func<Vector2Int, int> onTileCostGetter;   // 指定座標の移動コストを取得する(必要であれば)
			public bool useDiagonal = true; // 斜めを使用する

			public Utility.Async.BaseAwaiter awaiter = null;

			// Debug
			public System.Action<Node> onCreatedNode;
		}

		public class Node
		{
			public Node parent; // 親ノード

			public Vector2Int pos;
			public int index;
			public int cost;    // 実コスト
			public int estimatedCost;   // 推定コスト
			public int score;   // スコア
			public int count;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		// Nodeはキャッシュして使い回す
		private Stack<Node> _usedNodes = new Stack<Node>();
		private Stack<Node> _unusedNodes = new Stack<Node>();

		private SortedDictionary<int, List<Node>> _openedNodes = new SortedDictionary<int, List<Node>>();
		private HashSet<int> _usedIndexes = new HashSet<int>(); // 使用済みの座標

		private Param _param;
		private Node _firstNode, _lastNode;

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
			ExexuteInternalAsync(param).Forget();
			return IsSuccess;
		}

		/// <summary>
		/// 間隔を開けて走査する
		/// </summary>
		public async UniTask<bool> DebugExecuteAsync(Param param)
		{
			if (param.awaiter == null)
			{
				var awaiter = new Utility.Async.TimeAwaiter();
				awaiter.Setup(0.2f);

				param.awaiter = awaiter;
			}

			await ExexuteInternalAsync(param);
			return IsSuccess;
		}

		/// <summary>
		/// 探索が正常に終わっている場合、スコアとルート座標を取得する
		/// </summary>
		public bool TryGetScoreAndPositions(out int score, out List<Vector2Int> positions)
		{
			score = 0;
			positions = null;

			// 失敗している場合はfalse
			if (!IsSuccess) return false;

			score = _lastNode.score;
			positions = GetRoutePositions();

			return true;
		}

		/// <summary>
		/// 到達したルートのNodeリストを取得する
		/// </summary>
		public List<Node> GetRouteNodes()
		{
			if (!IsSuccess) return null;
			if (_lastNode == null) return null;

			var result = new List<Node>(_lastNode.count);

			var node = _lastNode;
			while (node != null)
			{
				result.Insert(0, node);
				node = node.parent;
			}

			return result;
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 実際のルートを見つける処理の実行
		/// </summary>
		private async UniTask<bool> ExexuteInternalAsync(Param param)
		{
			_param = param;
			IsSuccess = false;

			ResetNodeAll();

			_openedNodes.Clear();
			_usedIndexes.Clear();
			_lastNode = null;

			// 最初のNodeを作成する
			_firstNode = PopNode();
			_firstNode.pos = _param.start;
			_firstNode.index = _param.start.y * _param.width + _param.start.x; ;
			_firstNode.count = 1;

			// Nodeの作成に失敗したときは何もしない
			if (_firstNode == null)
			{
				Utility.Log.Error("開始位置のNodeの作成に失敗しました");
				return false;
			}

			CalcScore(_firstNode, 0);

			_param.onCreatedNode?.Invoke(_firstNode);

			await ExecuteInternalAsync(_firstNode, param.awaiter);

			return IsSuccess;
		}

		private async UniTask ExecuteInternalAsync(Node node, Utility.Async.BaseAwaiter awaiter)
		{
			var cost = node.cost;

			// 周りを開ける
			var dirMap = Utility.Map.GetDirArray(_param.useDiagonal);
			for (int i = 0, size = dirMap.GetLength(0); i < size; ++i)
			{
				var addX = dirMap[i, 0];
				var addY = dirMap[i, 1];
				var pos = new Vector2Int(node.pos.x + addX, node.pos.y + addY);

				var childNode = CreateNode(node, addX, addY, node.count + 1);
				if (childNode == null) continue;

				CalcScore(childNode, cost);

				_param.onCreatedNode?.Invoke(childNode);

				// もしゴール地点なら終了！
				if (_param.end == pos)
				{
					_lastNode = childNode;
					IsSuccess = true;
					return;
				}

				if (awaiter != null)
				{
					await awaiter.Wait();
				}
			}

			if (_lastNode != null)
			{
				return;
			}

			// 一番スコアが低いNodeから走査していく
			var nodes = _openedNodes.FirstOrDefault().Value;
			if (nodes.IsNullOrEmpty())
			{
				Utility.Log.Warning($"Nodeが残っていない {_firstNode.pos}");
				return;
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

			await ExecuteInternalAsync(rootNode, awaiter);
		}

		/// <summary>
		/// Nodeを作成する
		/// </summary>
		/// <param name="posX">Node X座標</param>
		/// <param name="posY">Node Y座標<</param>
		/// <param name="parent">親Node</param>
		/// <returns></returns>
		private Node CreateNode(Node parent, int addX, int addY, int count)
		{
			// すでに一度通った場所は何もしない
			var pos = new Vector2Int(parent.pos.x + addX, parent.pos.y + addY);
			var index = pos.y * _param.width + pos.x;
			if (_usedIndexes.Contains(index)) return null;

			// 移動できない場合は何もしない
			// 斜めの場合は斜め移動できるかを見る
			bool isSuccess = false;
			bool isAddUsedIndex = true; // 登録するか

			do
			{
				if (!_param.onCanMove(pos))
				{
					break;
				}

				if (addX != 0 && addY != 0)
				{
					if (!_param.onCanDiagonalMove(new Vector2Int(parent.pos.x + addX, parent.pos.y)))
					{
						isAddUsedIndex = false; // 斜め移動できないときは登録はしない
						break;
					}

					if (!_param.onCanDiagonalMove(new Vector2Int(parent.pos.x, parent.pos.y + addY)))
					{
						isAddUsedIndex = false;     // 斜め移動できないときは登録はしない
						break;
					}
				}

				isSuccess = true;

			} while (false);

			if (!isSuccess)
			{
				if (isAddUsedIndex)
				{
					_usedIndexes.Add(index);
				}

				return null;
			}

			var node = PopNode();
			node.pos = pos;
			node.index = index;
			node.count = count;

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
			var addCost = _param.onTileCostGetter?.Invoke(node.pos) ?? 1;
			node.cost = cost + addCost;

			// 推定コスト
			node.estimatedCost = CalcEstimatedCost(node.pos.x, node.pos.y);

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
			var index = node.pos.y * _param.width + node.pos.x;
			_usedIndexes.Add(index);
		}

		/// <summary>
		/// 推定コストを求める
		/// </summary>
		private int CalcEstimatedCost(int x, int y)
		{
			var dx = (int)Mathf.Abs(_param.end.x - x);
			var dy = (int)Mathf.Abs(_param.end.y - y);

			if (_param.useDiagonal)
			{
				// 斜めを許可している場合比較して高い値が推定コストとなる
				// 距離にしてみるか
				return dx * dx + dy + dy;
				//return Mathf.Max(dx, dy);
			}
			else
			{
				// 斜めを許可していない場合、推定コストはdx+dy
				return dx + dy;
			}
		}

		/// <summary>
		/// 探索終わったルートを取得する
		/// </summary>
		private List<Vector2Int> GetRoutePositions()
		{
			if (!IsSuccess) return null;
			if (_lastNode == null) return null;

			var result = new List<Vector2Int>(_lastNode.count - 1);

			var node = _lastNode;
			while (node != null)
			{
				// 最初のノードは入れない
				if (node.parent == null) break;

				result.Insert(0, node.pos);
				node = node.parent;
			}

			return result;
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
