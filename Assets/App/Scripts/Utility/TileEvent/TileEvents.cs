// 
// TileEvents.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.10.04.
// 

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Ling.Utility.Extensions;
using System.Linq;


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

        [SerializeField] private List<TileEvent> _tiles = new List<TileEvent>();

        #endregion


        #region プロパティ

        /// <summary>
        /// すべてのイベントが含まれる
        /// </summary>
        public List<TileEvent> Tiles { get; private set; } = new List<TileEvent>();

        #endregion


        #region public, protected 関数

        public void SetDeleted(bool isEditor = false)
        {
            Init(isEditor);

            foreach(var elm in Tiles)
            {
                elm.Deleted = true;
            }
        }

        /// <summary>
        /// タイルリストを作成する
        /// </summary>
        /// <param name="isEditor"></param>
        public void CreateTilesList(bool isEditor = false)
        {
            Init(isEditor);

            var bounds = _eventsMap.cellBounds;
            var offsetX = bounds.x - _lastMapBounds.x;
            var offsetY = bounds.y - _lastMapBounds.y;

            _lastMapBounds = bounds;

            // GetTilesBlock : 指定された範囲に存在するタイルを配列で返す
            var allTiles = _eventsMap.GetTilesBlock(_eventsMap.cellBounds);

            if (offsetX != 0 || offsetY != 0)
            {
                foreach(var elm in Tiles)
                {
                    elm.SetPosition(_eventsMap, elm.PosX - offsetX, elm.PosY - offsetY);
                }
            }

            bool isAdd = false;
            for (var x = 0; x < bounds.size.x; ++x)
            {
                for (var y = 0; y < bounds.size.y; y++)
                {
                    var tile = allTiles[x + y * bounds.size.x];
                    if (tile == null || Contains(x, y))
                    {
                        continue;
                    }

                    Tiles.Add(TileEvent.CreateEvent(tile, _eventsMap, x, y));
                    isAdd = true;
                }
            }

            if (isAdd)
            {
                Tiles = Tiles.OrderBy(t => t.PosX).ThenBy(t => t.PosY).ToList();
            }
        }


        /// <summary>
        /// リストに含まれているか確認
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(int x, int y)
        {
            foreach(var tile in Tiles)
            {
                if (tile.PosX == x && tile.PosY == y)
                {
                    tile.Deleted = false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 削除するタイルをすべて消す
        /// </summary>
        /// <param name="isEditor"></param>
        public void RemoveDeleteTiles(bool isEditor = false)
        {
            Init(isEditor);

            var bounds = _eventsMap.cellBounds;
            var offsetX = bounds.x - _lastMapBounds.x;
            var offsetY = bounds.y - _lastMapBounds.y;

            _lastMapBounds = bounds;

            var allTiles = _eventsMap.GetTilesBlock(bounds);

            if (offsetX != 0 || offsetY != 0)
            {
                foreach(var elm in Tiles)
                {
                    elm.SetPosition(_eventsMap, elm.PosX - offsetX, elm.PosY - offsetY);
                }
            }

            for (int x = 0; x < bounds.size.x; ++x)
            {
                for (int y = 0; y < bounds.size.y; ++y)
                {
                    var tile = allTiles[x + y * bounds.size.x];

                    if (tile != null)
                    {
                        Contains(x, y);
                    }
                }
            }

            Tiles.RemoveAll(tile => tile.Deleted);
        }

        /// <summary>
        /// イベントを叩く
        /// </summary>
        public void FireInteractible()
        {
            foreach (var elm in Tiles)
            {
                if (elm.Trigger != EventTriggerType.OnInteraction)
                {
                    continue;
                }

                elm.Interact();
            }
        }

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

            // イベントマップがプレイヤーの移動をブロックしなくなる
            _eventsCollider.isTrigger = true;

            _isInitialized = true;
        }

        #endregion


        #region MonoBegaviour


        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (var tile in Tiles)
            {
                var cellSize = _eventsMap.cellSize.Multiple(_eventsMap.transform.lossyScale);

                var position = new Vector3(tile.WorldX, tile.WorldY/* + cellSize.y*/);
                var bounds = new Bounds(position, cellSize);

                if (!bounds.Intersects(collision.bounds))
                {
                    continue;
                }

#if DEBUG
                Utility.Log.Print(tile.ToString());
#endif

                if (tile.OnEvent == null)
                {
                    continue;
                }

                switch (tile.Trigger)
                {
                    case EventTriggerType.OnEnterCollision:
                        if (tile.InteractibleTag == string.Empty ||
                            tile.InteractibleTag == collision.tag)
                        {
                            tile.OnEvent.Invoke();
                        }
                        break;

                    case EventTriggerType.OnInteraction:
                        if (tile.InteractibleTag == string.Empty ||
                            tile.InteractibleTag == collision.tag)
                        {
                            tile.SetInsteractible(true);
                        }
                        break;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            foreach(var elm in Tiles)
            {
                elm.SetInsteractible(false);
            }
        }

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
            Init();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            // イベントタイルを見えなくなる
            // ゲームモード時
            //_eventsRenderer.sortingLayerName = "Default";

            // イベントがプレイヤーをブロックしなくなる
            //_eventsCollider.isTrigger = true;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        void OnDestroy()
        {
        }

#endregion
    }
}