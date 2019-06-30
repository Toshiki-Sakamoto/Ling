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
        /// タップされたらtrue
        /// </summary>
        /// <value><c>true</c> if is tap; otherwise, <c>false</c>.</value>
        public bool IsTap { get; private set; }

        /// <summary>
        /// Viewを返す
        /// </summary>
        /// <value>The view.</value>
        public View View { get; private set; }

        /// <summary>
        /// Windowインスタンス
        /// </summary>
        /// <value>The window.</value>
        public Window.View Win { get { return View.Win; } }

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
                    AdvStop(); 
                };
        }

        /// <summary>
        /// アドベンチャーを開始する
        /// </summary>
        public void AdvStart()
        {
            Utility.Event.SafeTrigger(new EventStart());

            // 処理を開始する
            if (Cmd.Command.Count == 0)
            {
                AdvStop();
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
        public void AdvStop()
        {
            IsPlaying = false;

            StopAllCoroutines();

            Utility.Event.SafeTrigger(new EventStop());
        }

        /// <summary>
        /// 指定したラベルまで移動する
        /// </summary>
        public void GotoLabel(Command.LabelRef labelRef)
        { 
            // ジャンプがあるか
            if (labelRef.Jump == null)
            {
                Utility.Log.Error("ジャンプ先がない {0}", labelRef.Name);
                return; 
            }

            for (int i = 0; i < Cmd.Command.Count; ++i)
            {
                if (Cmd.Command[i] != labelRef.Jump)
                {
                    continue; 
                }

                _cmdIndex = i;

                return;
            }

            Utility.Log.Error("ジャンプ先のラベルが見つからない {0}", labelRef.Name);
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
                IsTap = false;

                if (_cmdIndex >= Cmd.Command.Count)
                {
                    break;
                }

                // コマンドを進める
                var cmd = Cmd.Command[_cmdIndex++];

                var process = cmd.Process();
                while (process.MoveNext())
                {
                    IsTap = false;

                    yield return null; 
                }

                // タップ待機するか
                if (cmd.IsTapWait())
                {
                    IsTap = false;

                    // オート設定なってたら別

                    while (!IsTap)
                    {
                        yield return null; 
                    }
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
            EventManager.Instance.Setup();

            // View 
            View = View.Create(_trsWindowRoot);
            View.Setup();


            // 画面がタップされた
            Utility.Event.SafeAdd<Window.EventWindowTap>(this,
                (ev_) => 
                {
                    IsTap = true;
                });

            // Window開く
            Utility.Event.SafeAdd<Window.EventWindowOpen>(this,
                (obj_) =>
                {
                    View.Show();
                });

            // 閉じる
            Utility.Event.SafeAdd<Window.EventHide>(this,
                (ev_) =>
                {
                    View.Hide(ev_);
                });
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
