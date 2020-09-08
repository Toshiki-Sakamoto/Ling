//
// ProcessMove.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Ling.Chara.Process
{
	/// <summary>
	/// キャラクタの移動
	/// </summary>
	public class ProcessMove : Utility.ProcessBase
    {
		#region 定数, class, enum

		public enum Type
		{
			Add, Set,
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ViewBase _charaView;	// 移動対象のキャラ
		private Type _type;
		private Vector2Int _startPos;
		private Vector2Int _endPos;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 通常の移動
		/// </summary>
		public void SetAddPos(Chara.ViewBase charaView, in Vector2Int addPos)
		{
			_charaView = charaView;
			_endPos = addPos;
			_type = Type.Add;
		}

		public void SetPos(Chara.ViewBase charaView, in Vector2Int startPos, in Vector2Int endPos)
		{
			_charaView = charaView;
			_startPos = startPos;
			_endPos = endPos;
			_type = Type.Set;
		}

		protected override void ProcessStartInternal()
		{
			// 指定座標に移動させる
			if (_type == Type.Add)
			{
				_charaView
					.MoveAtAddPos(_endPos)
					.Subscribe(_ => ProcessFinish());
			}
			else
			{
				_charaView
					.Move(_startPos, _endPos)
					.Subscribe(_ => ProcessFinish());
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
