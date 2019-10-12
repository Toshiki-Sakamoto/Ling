// 
// TileEvents.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.10.04.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace Ling.Utility.TileEvent
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Tilemap))]
    [RequireComponent(typeof(TilemapRenderer))]
    [RequireComponent(typeof(TilemapCollider2D))]
    public class TileEvents : MonoBehaviour
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        private bool _isInitialized;
        private Tilemap _eventsMap;
        private BoundsInt _lastMapBounds;
        private TilemapRenderer _eventsRenderer;
        private TilemapCollider2D _eventsCollider;

        //private List<EventTile> _tiles = new List<EventTile>();

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        private void Init(bool isEditor = false)
        {
            if (_isInitialized)
            {
                return;
            }

            _eventsMap = GetComponent<Tilemap>();
            _lastMapBounds = _eventsMap.cellBounds;
            _eventsRenderer = GetComponent<TilemapRenderer>();

            // エディターじゃない場合は隠す
            if (!isEditor)
            {
                _eventsRenderer.sortingLayerName = "Default";
                _eventsRenderer.sortingOrder = -9999;
            }

            _eventsCollider = GetComponent<TilemapCollider2D>();

            // 

            _eventsCollider.isTrigger = true;

            /*
            if (Tiles == null)
            {

            }*/

            _isInitialized = true;
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        void OnDestoroy()
        {
        }

        #endregion
    }
}