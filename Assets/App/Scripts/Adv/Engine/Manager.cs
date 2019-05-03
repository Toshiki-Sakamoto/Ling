//
// Manager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.24
//

using System;
using System.Collections;
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

        private int _cmdIndex = 0;  // 現在再生中のコマンドインデックス

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

        /// <summary>
        /// Viewを返す
        /// </summary>
        /// <value>The view.</value>
        public Window.View View { get; private set; }

        /// <summary>
        /// Windowインスタンス
        /// </summary>
        /// <value>The window.</value>
        public Window.Window Win { get { return View.Win; } }

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

            // 終了時
            Cmd.ActCmdFinish = 
                () =>
                {
                    Stop(); 
                };
        }

        /// <summary>
        /// アドベンチャーを開始する
        /// </summary>
        public void Start()
        {
            Utility.Event.SafeTrigger(new EventStart());

            // 処理を開始する
            if (Cmd.Command.Count == 0)
            {
                Stop();
                return;
            }

            _cmdIndex = 0;
            IsPlaying = true;

            // 事前読み込み処理
            Cmd.Load();

            StartCoroutine(Process());
        }

        /// <summary>
        /// アドベンチャー終わり
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;

            StopAllCoroutines();

            Utility.Event.SafeTrigger(new EventStop());
        }


        #endregion


        #region private 関数

        /// <summary>
        /// 実装する
        /// </summary>
        /// <returns>The proecss.</returns>
        private IEnumerator Process()
        {
            do
            {
                if (_cmdIndex >= Cmd.Command.Count)
                {
                    break;
                }

                // コマンドを進める
                var cmd = Cmd.Command[_cmdIndex++];

                var process = cmd.Process();
                while (process.MoveNext())
                {
                    yield return null; 
                }

            } while (true); 
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Event管理者
            Command.EventManager.Instance.Setup();

            // View 
            View = Window.View.Create(_trsWindowRoot);
            View.Setup();
        }

        private void Update()
        {
            if (!IsPlaying)
            {
                return;
            }
        }

        private void OnDestroy()
        {
            Cmd.OnDestory();

            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion
    }
}
