// 
// TilemapInspector.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.16.
// 
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace Ling.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(Tilemap))]
    public class TilemapInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}