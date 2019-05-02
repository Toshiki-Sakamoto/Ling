//
// Manager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.24
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine
{
	/// <summary>
	/// 
	/// </summary>
    public class Manager : MonoBehaviour
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数


        #endregion


        #region private 変数

        [SerializeField] private Transform _trsWindowRoot = null;

        private Window.View _view = null;

        #endregion


        #region プロパティ

        public static Manager Instance { get; private set; }

        /// <summary>
        /// コマンド管理者
        /// </summary>
        /// <value>The command.</value>
        public Command.Manager Cmd { get; private set; } = new Command.Manager();
        
        /// <summary>
        /// 変数管理者
        /// </summary>
        public Value.Manager Value { get; private set; } = new Value.Manager();

        /// <summary>
        /// 再生中
        /// </summary>
        /// <value><c>true</c> if is playing; otherwise, <c>false</c>.</value>
        public bool IsPlaying { get; private set; }

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        /// <summary>
        /// ファイルからスクリプトを読み出す
        /// </summary>
        /// <param name="filename">Filename.</param>
        public void Load(string filename)
        {
            // テーブルにコマンドを格納する
            Cmd.Setup();
	        
            var creator = new Creator(Cmd, Value);
            creator.Read(filename);
            
            // 読み込んだコマンドを表示する
            Utility.Log.Print("-------- Command ------");

            foreach (var elm in Cmd.Command)
            {
	            Utility.Log.Print("{0}", elm.ToString());
            }
            
            Utility.Log.Print("-------- Command ------");
        }

        /// <summary>
        /// アドベンチャーを開始する
        /// </summary>
        public void Start()
        {
            Utility.Event.SafeTrigger(new EventStart());

            IsPlaying = true;
        }

        /// <summary>
        /// コマンドを進める
        /// </summary>
        public void Step()
        {

        }

        /// <summary>
        /// アドベンチャー終わり
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;

            Utility.Event.SafeTrigger(new EventStop());
        }


        public void Update()
        {
            if (IsPlaying)
            {
                return;
            } 
        }

        #endregion


        #region private 関数

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // View 
            _view = Window.View.Create(_trsWindowRoot);
            _view.Setup();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion
    }
}
