//
// Data.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.28
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine.Value
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class Data
    {
        #region 定数, class, enum

        public enum ValueType
        {
	        None,	// 不明
	        String,	// 文字列
	        Int,	// int
	        Float,	// float

	        Value,	// 変数
        }

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        public abstract ValueType Type { get; }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        public virtual void Set(Data src) { }
        public virtual void Add(Data src) { }

        #endregion


        #region private 関数

        #endregion
    }

	public class ValueNone : Data
	{
		public override ValueType Type { get { return ValueType.None; } }
	}


	public class ValueString : Data
	{
		public override ValueType Type { get { return ValueType.String; } }
		
		public string Value { get; set; }


        public override void Set(Data src) 
        { 
            if (src.Type != ValueType.String)
            {
                return; 
            }

            Value = ((ValueString)src).Value;
        }

        public override void Add(Data src) 
        {
            if (src.Type != ValueType.String)
            {
                return;
            }

            Value += ((ValueString)src).Value;
        }
    }
	
	public class ValueInt : Data
	{
		public override ValueType Type { get { return ValueType.Int; } }

        public int Value { get; set; }

        /// <summary>
        /// 符号をマイナスにする
        /// </summary>
        public void Change()
        {
            Value *= -1;
        }

        public override void Set(Data src)
        {
            if (src.Type != ValueType.Int)
            {
                return;
            }

            Value = ((ValueInt)src).Value;
        }

        public override void Add(Data src)
        {
            if (src.Type != ValueType.Int)
            {
                return;
            }

            Value = ((ValueInt)src).Value;
        }
    }
	
	public class ValueFloat : Data
	{
		public override ValueType Type { get { return ValueType.Float; } }

        public float Value { get; set; }

        /// <summary>
        /// 符号をマイナスにする
        /// </summary>
        public void Change()
        {
            Value *= -1;
        }

        public override void Set(Data src)
        {
            if (src.Type != ValueType.Float)
            {
                return;
            }

            Value = ((ValueFloat)src).Value;
        }

        public override void Add(Data src)
        {
            if (src.Type != ValueType.Float)
            {
                return;
            }

            Value = ((ValueFloat)src).Value;
        }
    }
	
	public class Value : Data
	{
		public override ValueType Type { get { return ValueType.Value; } }
	}
}
