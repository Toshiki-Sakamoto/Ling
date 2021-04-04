// 
// AnimationStateController.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.18.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Chara
{
    /// <summary>
    /// 
    /// </summary>
    public class AnimationStateController : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Animator _animator = null;
        [SerializeField] private Rigidbody2D _rigidbody = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        private void SetStateToAnimator(Vector2? vec)
        {
            if (!vec.HasValue || vec == Vector2.zero)
            {
                //_animator.speed = 0.0f;
                return;
            }

//            _animator.speed = 1.0f;
            _animator.SetFloat("x", vec.Value.x);
            _animator.SetFloat("y", vec.Value.y);
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            // RigidBodyのvelocityから向きを求める
            var velocity = _rigidbody.velocity;


            SetStateToAnimator(velocity);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        void OnDestroy()
        {
        }

        #endregion
    }
}