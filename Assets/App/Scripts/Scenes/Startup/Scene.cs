// 
// Scene.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Startup
{
    /// <summary>
    /// 
    /// </summary>
    public class Scene : Common.Scene.Base
	{
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private View _view = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数


        /// <summary>
        /// 更新処理
        /// </summary>
        private void StateMainInit()
        {
            _view.ActOnClick = 
                () =>
                {
                    Common.Scene.Manager.Instance.Change(Common.Scene.SceneID.Battle);   
                };
        }

        private void StateMainProc()
        {
        }

        private void StateMainTerm()
        { 
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
			// Scene.Managerに登録をする
			Common.Scene.Manager.Instance.StartScene(this);
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