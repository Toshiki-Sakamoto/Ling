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

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数


        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
		{
			// 最初に遷移するSceneを選ぶ
			_sceneManager.StartScene(this, Common.Scene.SceneID.Title);
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