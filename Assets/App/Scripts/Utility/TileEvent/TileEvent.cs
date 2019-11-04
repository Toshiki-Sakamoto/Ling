//
// TileEvent.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.10.04
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace Ling.Utility.TileEvent
{
    public enum EventTriggerType
    {
        OnEnterCollision = 0,
        OnInteraction = 1,
    }


	/// <summary>
	/// 
	/// </summary>
    [System.Serializable]
    public class TileEvent
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        [SerializeField] private string _name = null;  // タイル名
        [SerializeField] private EventTriggerType _trigger;
        [SerializeField] private UnityEvent _onEvent;
        [SerializeField] private string _interactibleTag = null;    // イベントを識別するタグ
        

        private TileBase _tile = null;
        private bool _isInteractible;

        #endregion


        #region プロパティ

        /// <summary>
        /// タイルマップ内の座標
        /// </summary>
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        /// <summary>
        /// world座標
        /// </summary>
        public float WorldX { get; private set; }
        public float WorldY { get; private set; }

        public UnityEvent OnEvent { get { return _onEvent; } set { _onEvent = value; } }

        public EventTriggerType Trigger { get { return _trigger; } }

        public string InteractibleTag { get { return _interactibleTag; } }

        /// <summary>
        /// エディターのみ : 削除されたかどうかをみる
        /// </summary>
        public bool Deleted { get; set; }

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="eventsMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TileEvent CreateEvent(TileBase tile, Tilemap eventsMap, int x, int y)
        {
            var instance = new TileEvent { _tile = tile };
            instance._name = "Tile " + x + ":" + y;

            return instance;
        }

        public void SetPosition(Tilemap eventsMap, int x, int y)
        {
            var place = eventsMap.CellToWorld(new Vector3Int(x + eventsMap.cellBounds.x, y, eventsMap.cellBounds.z));
            var position = new Vector3(place.x + 1, place.y - 1, place.z);

            PosX = x;
            PosY = y;
            WorldX = position.x;
            WorldY = position.y;
        }

        public void SetInsteractible(bool interactible = false)
        {
            if (_trigger != EventTriggerType.OnInteraction)
            {
                return;
            }

            _isInteractible = interactible;
        }

        public void Interact()
        { 
            if (_trigger != EventTriggerType.OnInteraction)
            {
                _onEvent.Invoke();
            }
            else if (_isInteractible)
            {
                _onEvent.Invoke();
            }
        }

        #endregion


        #region private 関数

        #endregion
    }
}
