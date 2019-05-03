//
// Events.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.03
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
    /// テキスト追加
    /// </summary>
    public class EventAddText : EventStackBase
    { 
        public string Text { get; set; }

        public override void Clear()
        {
            Text = string.Empty;
        }
    }

}
