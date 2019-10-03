using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace NTW.TilemapEvents
{
    /// <summary>
    /// TilemapEvents class.
    /// </summary>
    [RequireComponent(typeof(Tilemap))]
    [RequireComponent(typeof(TilemapRenderer))]
    [RequireComponent(typeof(TilemapCollider2D))]
    public class TilemapEvents : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Contains all the event tiles of the scene.
        /// </summary>
        public List<EventTile> Tiles;

        private bool initialized;
        private Tilemap eventsMap;
        private BoundsInt lastMapBounds;
        private TilemapRenderer eventsRenderer;
        private TilemapCollider2D eventsCollider;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Used to initialization during the game mode.
        /// </summary>
        private void Start() { Init(); }

        private void Init(bool editor = false)
        {
            if (initialized) return;

            eventsMap = GetComponent<Tilemap>();
            lastMapBounds = eventsMap.cellBounds;
            eventsRenderer = GetComponent<TilemapRenderer>();
            /**
            * This makes sure that the event tiles are not shown
            * in the scene during the game mode.
            */
            if (!editor)
            {
                eventsRenderer.sortingLayerName = "Default";
                eventsRenderer.sortingOrder = -9999;
            }

            eventsCollider = GetComponent<TilemapCollider2D>();
            /**
             * This makes sure that the event won't block the player
             * on colliding during the game mode.
             */
            eventsCollider.isTrigger = true;

            if (Tiles == null) Tiles = new List<EventTile>();

            initialized = true;
        }

        /// <summary>
        /// Called once per frame during the game mode.
        /// </summary>
        private void Update()
        {
            /**
             * This makes sure that the event tiles are not shown
             * in the scene during the game mode.
             */
            eventsRenderer.sortingLayerName = "Default";
            /**
             * This makes sure that the event won't block the player
             * on colliding during the game mode.
             */
            eventsCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var tile in Tiles)
            {
                var position = new Vector3(tile.worldX,
                    tile.worldY + eventsMap.cellSize.y);
                var bounds = new Bounds(position, eventsMap.cellSize);

                if (bounds.Intersects(col.bounds))
                {
                    if (tile.onEvent != null)
                    {
                        switch (tile.Trigger)
                        {
                            case EventTriggerType.OnEnterCollision:
                                if (tile.InteractibleTag == string.Empty ||
                                    tile.InteractibleTag == col.tag)
                                    tile.onEvent.Invoke();
                                break;
                            case EventTriggerType.OnInteraction:
                                if (tile.InteractibleTag == string.Empty ||
                                    tile.InteractibleTag == col.tag)
                                    tile.SetInteractible(true);
                                break;
                        }
                    }
                }
            }
        }

        /**
         * Not actually supported.
         */
        //private void OnMouseDown()
        //{
        //    Debug.Log(Input.mousePosition);
        //}

        private void OnTriggerExit2D(Collider2D col)
        {
            Tiles.All
            (
                (tile) =>
                {
                    tile.SetInteractible(false);
                    return true;
                }
            );
        }

        /// <summary>
        /// Set all the tiles to deleted.
        /// </summary>
        /// <param name="editor">True if in editor mode.</param>
        public void SetDeleted(bool editor = false)
        {
            Init(editor);
            Tiles.All(tile => tile.Deleted = true);
        }

        /// <summary>
        /// Creates the Tiles list.
        /// </summary>
        /// <param name="editor">True if in editor mode.</param>
        public void CreateTilesList(bool editor = false)
        {
            Init(editor);

            var bounds = eventsMap.cellBounds;
            var offsetX = bounds.x - lastMapBounds.x;
            var offsetY = bounds.y - lastMapBounds.y;
            lastMapBounds = bounds;
            var allTiles = eventsMap.GetTilesBlock(eventsMap.cellBounds);

            if (offsetX != 0 || offsetY != 0)
            {
                Tiles.All
                (
                    t =>
                    {
                        t.SetPosition(eventsMap, t.X - offsetX, t.Y - offsetY);
                        return true;
                    }
                );
            }

            for (var x = 0; x < bounds.size.x; x++)
            {
                for (var y = 0; y < bounds.size.y; y++)
                {
                    var tile = allTiles[x + y * bounds.size.x];

                    if (tile != null && !Contains(x, y))
                    {
                        Tiles.Add(EventTile.CreateEvent(tile, eventsMap, x, y));
                        Tiles = Tiles.OrderBy(t => t.X).ThenBy(t => t.Y).ToList();
                    }
                }
            }
        }

        /// <summary>
        /// Check if a tile is contained in the list.
        /// </summary>
        /// <returns>Returns whether if the tile is contained or not.</returns>
        public bool Contains(int x, int y)
        {
            var result = false;

            foreach (var tile in Tiles)
            {
                if (tile.X == x && tile.Y == y)
                {
                    tile.Deleted = false;
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes from the Tiles list the deleted tiles.
        /// </summary>
        /// <param name="editor">True if in editor mode.</param>
        public void RemoveDeleteTiles(bool editor = false)
        {
            Init(editor);

            var bounds = eventsMap.cellBounds;
            var offsetX = bounds.x - lastMapBounds.x;
            var offsetY = bounds.y - lastMapBounds.y;
            lastMapBounds = bounds;
            var allTiles = eventsMap.GetTilesBlock(bounds);

            if (offsetX != 0 || offsetY != 0)
            {
                Tiles.All
                (
                    t =>
                    {
                        t.SetPosition(eventsMap, t.X - offsetX, t.Y - offsetY);
                        return true;
                    }
                );
            }

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    var tile = allTiles[x + y * bounds.size.x];

                    if (tile != null) Contains(x, y);
                }
            }

            Tiles.RemoveAll(tile => tile.Deleted);
        }

        /// <summary>
        /// Fires the active interactible events.
        /// </summary>
        public void FireInteractible()
        {
            foreach (var tile in Tiles)
            {
                if (tile.Trigger != EventTriggerType.OnInteraction)
                    continue;

                tile.Interact();
            }
        }

        #endregion Methods
    }
}