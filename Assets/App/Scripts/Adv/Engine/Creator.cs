//
// Create.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.26
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class Creator
    {
        #region 定数, class, enum


        public struct ValueOrNumber
        {
            public bool isValue;
            public int value;
        }

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private Manager _manager = null;
        private Reader _reader = null;

        private List<Label> _label = new List<Label>();
        private List<string> _valueName = new List<string>();

        private uint _thenIndex = 0;

        #endregion


        #region プロパティ

        /// <summary>
        /// Gets the reader.
        /// </summary>
        /// <value>The reader.</value>
        public Reader Reader { get { return _reader; } }

        public Stack<uint> ThenNest { get; private set; } = new Stack<uint>();

        #endregion


        #region コンストラクタ, デストラクタ

        public Creator(Manager manager)
        {
            _manager = manager;
        }

        #endregion


        #region public, protected 関数



        /// <summary>
        /// スクリプトの変換
        /// </summary>
        /// <param name="name">Name.</param>
        public void Read(string name)
        {
            _reader = new Reader();

            if (!_reader.Open(name))
            {

                return;
            }

            try
            {
                string str;
                while (!string.IsNullOrEmpty(str = _reader.GetString()))
                {
                    Parser(str);
                }
            }
            catch (Exception e)
            {
                Log.Error("{0}", e.Message);
            }
        }


        /// <summary>
        /// トークンに分割する
        /// 0だった場合は有効なトークンではないので何もしない
        /// </summary>
        /// <param name="str">String.</param>
        private void Parser(string str)
        {
            var lexer = new Lexer();
            lexer.Process(str);

            if (lexer.NumToken == 0)
            {
                // 有効なトークンではなかった
                return;
            }

            var type = lexer.GetTokenType();

            if (type == Lexer.TokenType.IsLable)
            {
                SetLabel(lexer);
            }
            else
            {

            }
        }

        /// <summary>
        /// スクリプトコマンドの解析
        /// </summary>
        /// <returns>The command.</returns>
        /// <param name="lexer">Lexer.</param>
        private Command.Base ParseCommand(Lexer lexer)
        {
            var cmd = lexer.GetString(0);

            var cmdManager = _manager;

            var cmdInstance = cmdManager.Create(cmd, lexer);
            if (cmdInstance != null)
            {
                return cmdInstance;
            }

            // set, calc の省略形か
            if (lexer.NumToken >= 3)
            {
                var p = lexer.GetString(1);

                // 先頭に戻す
                lexer.GetTokenType(0);

                switch (p)
                {
                    case "+":
                    case "-":
                    case "=":
                        return Command.Set.Create(this, lexer);
                }
            }

            Log.Error("syntax error (command syntax)");
            return null;
        }


        /// <summary>
        /// パラメータ確認して、正しければラベル登録する
        /// ラベルにはパラメータはないので1以外はエラー →　変更 2 つめにLabel名が入っている
        /// </summary>
        /// <param name="lexer">Lexer.</param>
        private void SetLabel(Lexer lexer)
        {
            if (lexer.NumToken != 2)
            {
                Log.Error("Labelに不正なパラメータがついてます");
                return;
            }

            var label = lexer.GetString(1);
            AddLabel(label);
        }


        /// <summary>
        /// ラベルの登録
        /// </summary>
        /// <param name="label">Label.</param>
        public void AddLabel(string label)
        {
            var labelCmd = new Command.Label();
            labelCmd.Name = label;
            labelCmd.Line = _reader.LineNo;

            foreach (var elm in _label)
            {
                // すでに登録されているか
                if (elm.Name != label)
                {
                    continue;
                }

                if (elm.IsPredefined)
                {
                    // すでに定義済み 
                    Log.Error("ラベルが二重に定義されてます {0}, line:{1}", elm.Name, elm.Line);
                    continue;
                }

                // ラベルが参照されている : 参照の解決をする
                var chain = elm.Reference;

                elm.Line = _reader.LineNo;
                elm.Reference = null;
                elm.Jump = labelCmd;

                while (chain != null)
                {
                    var next = chain.Next;

                    chain.LabelRef.Jump = labelCmd;
                    chain = next;
                }

                // コマンドとして追加
                _manager.AddCommand(labelCmd);

                return;
            }


            // コマンドとして追加
            _manager.AddCommand(labelCmd);
        }

        /// <summary>
        /// ラベルの参照
        /// </summary>
        /// <param name="label">Label.</param>
        public void FindLabel(string label, Label labelCmd)
        {
            foreach (var elm in _label)
            {
                if (elm.Name != label)
                {
                    continue;
                }

                // ラベルが参照されている
                if (elm.IsPredefined)
                {
                    // 参照に追加(Gotoコマンド）
                    elm.Reference = new Label.Ref(labelCmd, elm.Reference);
                }
                else
                {
                    // ラベルが登録されている
                    labelCmd.Jump = elm;
                }

                return;
            }

            // 新しいラベルを参照として登録
            var chain = new Label.Ref(labelCmd, null);

            labelCmd.Name = label;
            labelCmd.Reference = chain;

            _label.Add(labelCmd);
        }

        /// <summary>
        /// 変数名か数字文字列の取得と判別
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="lexer">Lexer.</param>
        public bool GetValueOrNumber(out ValueOrNumber value, Lexer lexer)
        {
            value = new ValueOrNumber();

            var type = lexer.GetTokenType();

            if (type == Lexer.TokenType.IsString)
            {
                var str = lexer.GetString();
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }

                value.value = FindValue(str);
                value.isValue = false;
            }
            else
            {
                if (!lexer.GetValue(out value.value))
                {
                    return false; 
                }

                value.isValue = true;
            }

            return true;
        }


        /// <summary>
        /// 比較演算子の判定
        /// </summary>
        /// <returns>The op.</returns>
        /// <param name="op">Op.</param>
        public ScriptType BoolOp(string op)
        {
            switch (op)
            {
                case "==":
                    return ScriptType.IF_TRUE_CMD;

                case "!=":
                    return ScriptType.IF_FALSE_CMD;

                case "<=":
                    return ScriptType.IF_SMALLER_EQU_CMD;

                case ">=":
                    return ScriptType.IF_BIGGER_EQU_CMD;

                case "<":
                    return ScriptType.IF_SMALLER_CMD;

                case ">":
                    return ScriptType.IF_BIGGER_CMD;

                default:
                    break;
            }

            Log.Error("構文エラー(比較演算子)");

            return ScriptType.NONE;
        }

        /// <summary>
        /// 比較演算子の判定（負論理）
        /// </summary>
        /// <returns>The op.</returns>
        /// <param name="op">Op.</param>
        public ScriptType NegBoolOp(string op)
        {
            switch (op)
            {
                case "==":
                    return ScriptType.IF_FALSE_CMD;

                case "!=":
                    return ScriptType.IF_TRUE_CMD;

                case "<=":
                    return ScriptType.IF_BIGGER_CMD;

                case ">=":
                    return ScriptType.IF_SMALLER_CMD;

                case "<":
                    return ScriptType.IF_BIGGER_EQU_CMD;

                case ">":
                    return ScriptType.IF_SMALLER_EQU_CMD;

                default:
                    break;
            }

            Log.Error("構文エラー(比較演算子)");

            return ScriptType.NONE;
        }

        /// <summary>
        /// if... then の内部ラベル生成
        /// </summary>
        /// <returns>The label.</returns>
        public string ThenLabel()
        {
            var index = _thenIndex++ << 16;

            ThenNest.Push(index);

            return FormatThenLabel(index);
        }

        /// <summary>
        /// Then index を文字列にする
        /// </summary>
        /// <returns>The then label.</returns>
        /// <param name="index">Index.</param>
        public string FormatThenLabel(uint index)
        {
             return string.Format("#endif#{0}", index);
        }

        #endregion


        #region private 関数

        /// <summary>
        /// 変数テーブルから変数の参照
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="value">Value.</param>
        private int FindValue(string value)
        {
            for (int i = 0; i < _valueName.Count; ++i)
            {
                if (value != _valueName[i])
                {
                    continue;
                }

                // 変数があったのでインデックスを返す
                return i;
            }

            // 変数を追加
            _valueName.Add(value);

            return _valueName.Count - 1;
        }

        private void LabelCheck()
        {
            foreach (var elm in _label)
            {
                if (elm.Reference != null)
                {
                    Log.Error("参照が解決されてないラベルがあります {0}", elm.Name); 
                }
            } 
        }

        #endregion
    }
}
