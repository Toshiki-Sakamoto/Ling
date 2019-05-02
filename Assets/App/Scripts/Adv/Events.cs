//
// Events.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.30
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv
{
    /// <summary>
    /// アドベンチャー開始
    /// </summary>
    public class EventStart : Utility.EventBase
    { 
    };

    /// <summary>
    /// アドベンチャー終了
    /// </summary>
    public class EventStop : Utility.EventBase
    { 
    };

    /// <summary>
    /// 読み込み終了
    /// </summary>
    public class EventLoad : Utility.EventBase
    { 
    };
}
