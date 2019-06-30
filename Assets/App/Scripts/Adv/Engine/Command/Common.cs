//
// Common.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.04
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
    public class Common
    {
        /// <summary>
        /// 短縮語がただしいかチェック
        /// </summary>
        /// <returns><c>true</c>, if short text check was ised, <c>false</c> otherwise.</returns>
        /// <param name="txtShort">Text short.</param>
        /// <param name="text">Text.</param>
        public static bool IsShortTextCheck(string txtShort, string text)
        {
            if (txtShort == text)
            {
                return true; 
            }

            switch (text)
            {
                case "only":
                case "Only":
                    {
                        switch(txtShort)
                        {
                            case "only":
                            case "Only":
                            case "o":
                                return true;

                            default:
                                return false; 
                        }
                    }

                default:
                    return false;
            }
        }
    }
}
