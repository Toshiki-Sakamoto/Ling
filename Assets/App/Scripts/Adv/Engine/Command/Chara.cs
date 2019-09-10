//
// Chara.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.09.09
//

using System;
using System.Collections;
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
    public class Chara : Base
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private List<string> _values = null;

        #endregion


        #region プロパティ

        /// <summary>
        /// コマンドタイプ
        /// </summary>
        /// <value>The type.</value>
        public override ScriptType Type { get { return ScriptType.CHARA_CMD; } }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        public static Chara Create(Creator creator, Lexer lexer)
        {
            var cmn = creator.CmdManager.Cmn;

            var str = creator.Reader.GetString(isAddEnd: false);

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            var instance = new Chara();

            instance._values = cmn.WhiteSpaceParse(str);


            creator.AddCommand(instance);


            return instance;
        }


        /// <summary>
        /// コマンド実行
        /// </summary>
        public override IEnumerator Process()
        {
            if (_values.Count < 2)
            {
                yield break;
            }

            var charaManager = Engine.Manager.Instance.Chara;

            // 指定したキャラが居るか
            var charaName = _values[0];

            var data = charaManager.GetData(charaName);
            if (data == null)
            {
                yield break;
            }

            // 表情を変える
            var faceFilename = data.GetFaceFilename(_values[1]);
            if (string.IsNullOrEmpty(faceFilename))
            {
                // デフォルトを使用する
            }

            string position = string.Empty;

            if (_values.Count >= 3)
            {
                position = _values[2];
            }
            

            yield break;
        }

        #endregion


        #region private 関数

        #endregion
    }
}
