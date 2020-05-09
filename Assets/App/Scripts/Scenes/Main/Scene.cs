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


namespace Ling.Main
{
    /// <summary>
    /// 
    /// </summary>
    public class Scene : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private View _view = null;

      //  private Utility.PhaseScene<Const.State> _phaseObj = new Utility.PhaseScene<Const.State>();

        #endregion


        #region プロパティ

        public View View { get { return _view; } }

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
     //       _phaseObj.Add(Const.State.Adv, Phase.Creator<Phase.Adv>.Create(this));
     //       _phaseObj.Add(Const.State.Start, Phase.Creator<Phase.Start>.Create(this));

     //       _phaseObj.Start(Const.State.Start);
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
         //   _phaseObj.Update();
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