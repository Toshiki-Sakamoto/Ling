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
		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ViewBase _charaView;	// 移動対象のキャラ
		private Vector2Int _addPos;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 通常の移動
		/// </summary>
		public void Setup(Chara.ViewBase charaView, in Vector2Int addPos)
		{
			_charaView = charaView;
			_addPos = addPos;
		}

		public void Start()
		{
			// 指定座標に移動させる
			_charaView
				.MoveByAddPos(_addPos)
				.Subscribe(_ => ProcessFinish());
		}

		#endregion


		#region private 関数

		#endregion
	}
}
