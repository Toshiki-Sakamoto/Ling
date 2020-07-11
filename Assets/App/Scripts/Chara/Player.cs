// 
// Player.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Ling.Chara
{
    /// <summary>
    /// PlayerView
    /// </summary>
    public class Player : Base 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ


        #endregion


        #region public, protected 関数

        /// <summary>
        /// 現在位置から指定した数を足して移動する
        /// </summary>
        /// <param name="addCellPos"></param>
        /// <param name="moveFinish"></param>
        public System.IObservable<AsyncUnit> MoveByAddPos(Vector3Int addCellPos) =>
            MoveController.SetMoveCellPos(CellPos + addCellPos);

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        #endregion
    }
}