// 
// CameraFollow.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using UnityEngine;

namespace Ling.Utility
{
    /// <summary>
    /// 指定したカメラを自動的に追従する
    /// </summary>
    public class CameraFollow : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Camera _followCamera = null;

        // カメラ座標と回転値
        [SerializeField] private Vector3 _position = default;
        [SerializeField] private Quaternion _initRotation = default;

        private Vector3 _cameraPos;

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

            // 角度
            //_cameraPos = _followCamera.transform.localPosition;

            //var angle = Mathf.Atan2(_cameraPos.y, _cameraPos.z) * Mathf.Rad2Deg;
            //_followCamera.transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);

            _cameraPos = _position;
            _followCamera.transform.rotation = _initRotation;
        }

        private void LateUpdate()
        {
            _followCamera.transform.position =
                new Vector3(transform.position.x,
                _cameraPos.y,
                transform.position.z + _cameraPos.z);//_followCamera.transform.position.z);
        }

        #endregion
    }
}