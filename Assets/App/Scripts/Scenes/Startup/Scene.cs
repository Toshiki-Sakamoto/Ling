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
    public class Scene : MonoBehaviour 
    {
        #region 定数, class, enum

        private enum State
        {
            Start,
            Main, 
        }

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private View _view = null;

        private Utility.Phase<State> _phase = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        /// <summary>
        /// 開始処理
        /// </summary>
        private void StateStartInit()
        { 
        }

        private void StateStartProc()
        {
            _phase.Set(State.Main);
        }

        private void StateStartTerm()
        { 
        }


        /// <summary>
        /// 更新処理
        /// </summary>
        private void StateMainInit()
        {
            _view.ActOnClick = 
                () =>
                {
                    Utility.Scene.Manager.Instance.Change(Common.Scene.ID.Battle);   
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
            _phase = Utility.Phase<State>.Create(this, State.Start);
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
            _phase.Update();
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