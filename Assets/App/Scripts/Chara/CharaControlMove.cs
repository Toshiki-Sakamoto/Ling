//
// CharaControlMove.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.12
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using Ling.Utility.Extensions;

namespace Ling.Chara
{
	public interface ICharaMoveController
	{
		/// <summary>
		/// 現在いる座標から指定した座標分移動する
		/// </summary>
		System.IObservable<AsyncUnit> MoveAtAddPos(in Vector2Int addCellPos);

		/// <summary>
		/// 指定座標まで移動する
		/// </summary>
		System.IObservable<AsyncUnit> Move(in Vector2Int startPos, in Vector2Int endPos);
	}

	/// <summary>
	/// CharaControlの動きに関するものを管理
	/// </summary>
	public partial class CharaControl<TModel, TView> : MonoBehaviour, ICharaController, ICharaMoveController
		where TModel : CharaModel
		where TView : ViewBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

        /// <summary>
        /// 現在位置から指定した数を足して移動する
        /// </summary>
        public System.IObservable<AsyncUnit> MoveAtAddPos(in Vector2Int addCellPos) =>
            CharaMover.SetMoveCellPos(_model.CellPosition.Value + addCellPos);

        public System.IObservable<AsyncUnit> Move(in Vector2Int startPos, in Vector2Int endPos) =>
            CharaMover.SetMoveCellPos(startPos, endPos);

		#endregion


		#region private 関数

		#endregion
	}
}
