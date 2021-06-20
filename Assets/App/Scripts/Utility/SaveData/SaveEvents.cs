//
// SaveEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.28
//

using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Utility.SaveData
{
    /// <summary>
    /// セーブ実行通知
    /// </summary>
    public class EventSaveCall
    {
    }

    /// <summary>
    /// データ読み込み通知
    /// </summary>
    public class EventLoadCall
    {
        public List<UniTask> LoadTasks = new List<UniTask>();


        public void Add(UniTask task)
        {
            LoadTasks.Add(task);
        }
    }
}
