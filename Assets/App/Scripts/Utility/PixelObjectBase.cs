// 
// PixelObjectBase.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.17.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common
{
    /// <summary>
    /// 描画時に小数点以下を考慮せずに、表示するときだけ座標を一時的にintにする
    /// </summary>
    public class PixelObjectBase : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        private Vector3 _cashPosition = Vector3.zero;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        private void LateUpdate()
        {
            _cashPosition = transform.localPosition;
            transform.localPosition = new Vector3(
                Mathf.RoundToInt(_cashPosition.x),
                Mathf.RoundToInt(_cashPosition.y),
                Mathf.RoundToInt(_cashPosition.z));
        }

        private void OnRenderObject()
        {
            transform.localPosition = _cashPosition;
        }

        #endregion
    }
}