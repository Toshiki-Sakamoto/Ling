// 
// Base.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Chara
{
    /// <summary>
    /// 
    /// </summary>
    public class Base : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数
        
        [SerializeField] private Animator _animator = null;
        [SerializeField] private Vector2Int _vecCellPos = Vector2Int.zero; // マップ上の自分の位置

        #endregion


        #region プロパティ

        /// <summary>
        /// マップ上の現在の位置
        /// </summary>
        public Vector2Int CellPos { get { return _vecCellPos; } }

        #endregion


        #region public, protected 関数


        /// <summary>
        /// 座標の設定
        /// </summary>
        /// <param name="pos"></param>
        public void SetCellPos(Vector2Int pos)
        {
            _vecCellPos = pos;
        }

        /// <summary>
        /// 向き
        /// </summary>
        /// <param name="dir"></param>
        public void SetDirection(Vector3 dir)
        {
            _animator.SetFloat("x", dir.x);
            _animator.SetFloat("y", dir.y);
        }


        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
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