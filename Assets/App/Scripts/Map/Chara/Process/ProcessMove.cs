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
	public class ProcessMove : Common.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ICharaMoveController _control;    // 移動対象のキャラ
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
		public void SetAddPos(Chara.ICharaMoveController control, in Vector2Int startPos, in Vector2Int addPos) =>
			SetPos(control, startPos, startPos + addPos);

		public void SetPos(Chara.ICharaMoveController control, in Vector2Int startPos, in Vector2Int endPos)
		{
			_control = control;
			_startPos = startPos;
			_endPos = endPos;
		}

		protected override void ProcessStartInternal()
		{
			// 指定座標に移動させる
			_control
				.Move(_startPos, _endPos)
				.Subscribe(_ => ProcessFinish());
		}

		#endregion


		#region private 関数

		#endregion
	}
}
