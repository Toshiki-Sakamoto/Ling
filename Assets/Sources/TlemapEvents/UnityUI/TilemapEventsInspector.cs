using UnityEditor;

namespace NTW.TilemapEvents.UnityUI
{
    /// <summary>
    /// Custom inspector class.
    /// </summary>
    [CustomEditor(typeof(TilemapEvents))]
    public class TilemapEventsInspector : Editor
    {
        /// <summary>
        /// Custom inspector entry point.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var map = (TilemapEvents)target;

            map.SetDeleted(true);
            map.CreateTilesList(true);
            map.RemoveDeleteTiles(true);

            DrawDefaultInspector();
        }
    }
}