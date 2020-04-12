// 
// Manager.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Ling.Utility.Scene
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        public static Manager Instance { get; private set; }

        #endregion


        #region private 変数

        private Common.Scene.ID _nextSceneID = Common.Scene.ID.None;
        private object _argment = null;
        private bool _isStart = false;


        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        public void Change(Common.Scene.ID sceneID, object arg = null)
        {
            if (_isStart)
            {
                Utility.Log.Warning($"シーンがすでに開始されています {sceneID.ToString()}");
                return; 
            }

			_nextSceneID = sceneID;
            _argment = arg;

            _isStart = true;

            StartCoroutine(StartScenInternal(_nextSceneID));
        }

        #endregion


        #region private 関数

        /// <summary>
        /// シーンを開始する
        /// </summary>
        /// <returns>The start scene.</returns>
        /// <param name="nextSceneName">Next scene name.</param>
        private IEnumerator StartScenInternal(Common.Scene.ID sceneID)
        {
            yield return SceneManager.LoadSceneAsync(Common.Scene.GetNameByID(sceneID));

            _isStart = false;
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance); 
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            if (Instance == this)
            {
                Instance = null; 
            }
        }

        #endregion
    }
}