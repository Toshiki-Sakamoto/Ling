// 
// CameraFollow.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraFollow : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Camera _followCamera = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        private void Start()
        {
            if (_followCamera == null)
            {
                _followCamera = Camera.main;
            }
        }

        private void LateUpdate()
        {
            _followCamera.transform.position = 
                new Vector3(transform.position.x, 
                transform.position.y, 
                _followCamera.transform.position.z);
        }

        #endregion
    }
}