using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace NTW.TilemapEvents
{
    /// <summary>
    /// Class which represents an event tile.
    /// It needs to derive from UnityEngine.Object to be
    /// serialized in the editor to show the Event field.
    /// </summary>
    [Serializable]
    public class EventTile
    {
        #region Properties

        /// <summary>
        /// The name of the tile.
        /// </summary>
        public string Name;

        /// <summary>
        /// The tag allowed to interact with the event.
        /// </summary>
        [Tag]
        public string InteractibleTag;

        /// <summary>
        /// The type of the trigger.
        /// </summary>
        public EventTriggerType Trigger;

        /// <summary>
        /// The associated tile.
        /// </summary>
        [HideInInspector]
        public TileBase Tile;

        /// <summary>
        /// The event to be fired;
        /// </summary>
        [SerializeField]
        public UnityEvent onEvent;

        /// <summary>
        /// The x position inside the tilemap.
        /// </summary>
        [HideInInspector]
        public int X;

        /// <summary>
        /// The y position inside the tilemap.
        /// </summary>
        [HideInInspector]
        public int Y;

        /// <summary>
        /// The x position inside the world.
        /// </summary>
        [HideInInspector]
        public float worldX;

        /// <summary>
        /// The y position inside the world.
        /// </summary>
        [HideInInspector]
        public float worldY;

        /// <summary>
        /// Editor only: determines if it should be
        /// deleted or not.
        /// </summary>

        [HideInInspector]
        public bool Deleted;

        private bool interactible;

        #endregion Properties

        #region Constuctors

        /// <summary>
        /// The constructor.
        /// </summary>
        public EventTile() { Deleted = true; }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates and return an event tile.
        /// </summary>
        /// <param name="tile">The base tile.</param>
        /// <param name="eventsMap">The event map where the tile sits in.</param>
        /// <param name="x">The x position inside the map (column of the map).</param>
        /// <param name="y">The y position inside the map (row of the map).</param>
        /// <returns>The created EventTile.</returns>
        public static EventTile CreateEvent(TileBase tile, Tilemap eventsMap, int x, int y)
        {
            var eventTile = new EventTile { Tile = tile };
            eventTile.Name = "Tile " + x + ":" + y;

            eventTile.SetPosition(eventsMap, x, y);

            return eventTile;
        }

        /// <summary>
        /// Sets the tile position.
        /// </summary>
        /// <param name="eventsMap">The tilemap to use to calculate the world coordinates.</param>
        /// <param name="x">X position relative to the tilemap.</param>
        /// <param name="y">Y position relative to the tilemap.</param>
        public void SetPosition(Tilemap eventsMap, int x, int y)
        {
            var place = eventsMap.CellToWorld(new Vector3Int(x + eventsMap.cellBounds.x, y, eventsMap.cellBounds.z));
            var position = new Vector3(place.x + 1, place.y - 1, place.z);

            X = x;
            Y = y;
            worldX = position.x;
            worldY = position.y;
        }

        /// <summary>
        /// Can be interacted or not?
        /// </summary>
        /// <param name="interactible">Set the interactible value.</param>
        public void SetInteractible(bool interactible = false)
        {
            if (Trigger != EventTriggerType.OnInteraction) return;

            this.interactible = interactible;
        }

        /// <summary>
        /// Fires the event!
        /// </summary>
        public void Interact()
        {
            /**
             * If it's not an interactible event
             * it can be fired at any time.
             */
            if (Trigger != EventTriggerType.OnInteraction)
                onEvent.Invoke();
            /**
             * If it's an interactible event first the
             * player should get close to it.
             */
            else if (interactible)
                onEvent.Invoke();
        }

        #endregion Methods
    }
}