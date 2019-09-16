// 
// MoveController.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.16.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common.Chara
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private Rigidbody2D _rigidBody = null;

        private Vector2 _inputAxis;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour


        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            // x, y の入力
            // 関連付けはInput Managerで行っている
            _inputAxis.x = Input.GetAxis("Horizontal");
            _inputAxis.y = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            _rigidBody.velocity = _inputAxis.normalized * _speed;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        void OnDestoroy()
        {
        }

        #endregion
    }
}