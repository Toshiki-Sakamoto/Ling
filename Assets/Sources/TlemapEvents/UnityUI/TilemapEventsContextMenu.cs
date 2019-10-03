using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace NTW.TilemapEvents.UnityUI
{
    public class TilemapEventsContextMenu
    {
        [MenuItem("GameObject/2D Object/Event Tilemap", false, 0)]
        public static void CreateTilemap()
        {
            var tilemap = new GameObject();
            tilemap.AddComponent<Tilemap>();
            tilemap.AddComponent<TilemapRenderer>();
            tilemap.AddComponent<TilemapEvents>();

            if (GameObject.Find("Events") == null) tilemap.name = "Events";
            else
            {
                var count = 1;

                while (GameObject.Find("Events " + count) != null)
                    ++count;

                tilemap.name = "Events " + count;
            }

            if (Selection.activeGameObject != null &&
                Selection.activeGameObject.GetComponent<Grid>() != null)
                tilemap.transform.parent = Selection.activeGameObject.transform;
            else
            {
                var grid = new GameObject();
                grid.AddComponent<Grid>();
                grid.name = "Grid";
                tilemap.transform.parent = grid.transform;
            }
        }
    }
}