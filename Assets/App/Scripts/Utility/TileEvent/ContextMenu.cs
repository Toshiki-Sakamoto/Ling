// 
// ContextMenu.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.11.10.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Ling.Utility.TileEvent
{
#if false
#if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    public class ContextMenu : MonoBehaviour 
    {
	#region 定数, class, enum

        [MenuItem("GameObject/2D Object/Tilemap Event", false, 0)]
        public static void CreateTilemap()
        {
            var tilemap = new GameObject();
            tilemap.AddComponent<Tilemap>();
            tilemap.AddComponent<TilemapRenderer>();
            tilemap.AddComponent<TileEvents>();

            if (GameObject.Find("Events") == null)
            {
                tilemap.name = "Events";
            }
            else
            {
                var count = 1;

                while (GameObject.Find("Events " + count) != null)
                {
                    ++count;
                }

                tilemap.name = "Events " + count;
            }

            if (Selection.activeGameObject != null &&
                Selection.activeGameObject.GetComponent<Grid>() != null)
            {
                tilemap.transform.parent = Selection.activeGameObject.transform;
            }
            else
            {
                var grid = new GameObject();
                grid.AddComponent<Grid>();
                grid.name = "Grid";
                tilemap.transform.parent = grid.transform;
            }
        }

	#endregion


	#region public 変数

	#endregion


	#region private 変数

	#endregion


	#region プロパティ

	#endregion


	#region public, protected 関数

	#endregion


	#region private 関数

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
        void OnDestroy()
        {
        }

	#endregion
    }
#endif
#endif
}