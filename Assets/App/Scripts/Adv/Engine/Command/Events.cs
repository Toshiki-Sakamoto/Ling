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
    public class EventStackBase : Utility.EventBase
    {
        public virtual void Clear() { } 
    }

    /// <summary>
    /// Windowを初期化する
    /// </summary>
    public class EventWindowClear : EventStackBase
    { 
    }

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


    /// <summary>
    /// イベント管理者
    /// </summary>
    public class EventManager : Utility.Singleton<EventManager>
    {
        private Dictionary<Type, EventStackBase> _dictEvents = new Dictionary<Type, EventStackBase>();


        public void Setup()
        {
            Add(new EventWindowClear());
            Add(new EventAddText());
        }

        /// <summary>
        /// イベントを登録
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <typeparam name="TEvent">The 1st type parameter.</typeparam>
        public void Add<TEvent>(TEvent instance) where TEvent : EventStackBase
        {
            var type = typeof(TEvent);

            if (_dictEvents.ContainsKey(type))
            {
                // すでに登録されている
                return;
            }

            _dictEvents[type] = instance;
        }

        /// <summary>
        /// 保持しているインスタンスのイベントを発行する
        /// </summary>
        /// <typeparam name="TEvent">The 1st type parameter.</typeparam>
        public void Trigger<TEvent>(Action<TEvent> act = null) where TEvent : EventStackBase
        {
            var type = typeof(TEvent);
            EventStackBase eventInstance = null;

            if (!_dictEvents.TryGetValue(type, out eventInstance))
            {
                Utility.Log.Warning("イベントが登録されていない {0}", type.Name);
                return; 
            }

            eventInstance.Clear();

            act?.Invoke((TEvent)eventInstance);

            // イベント発行
            Utility.Event.Instance.Trigger(eventInstance); 
        }

        /// <summary>
        /// Static版Trigger
        /// </summary>
        /// <param name="act">Act.</param>
        /// <typeparam name="TEvent">The 1st type parameter.</typeparam>
        public static void SafeTrigger<TEvent>(Action<TEvent> act = null) where TEvent : EventStackBase
        { 
            if (IsNull)
            {
                return; 
            }

            Instance.Trigger(act);
        }
    }
}
