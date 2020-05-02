// 
// MiniMapTileEditor.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Ling.Common.Tilemap;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ling.Editor.Tile
{
	/// <summary>
	/// 
	/// </summary>
#if UNITY_EDITOR
	[CustomEditor(typeof(MiniMapTile))]
	public class MiniMapTileEditor : UnityEditor.Editor
	{
		private MiniMapTile _tile = null;

		private MiniMapTile Tile
		{
			get
			{
				if (_tile == null)
				{
					_tile = target as MiniMapTile;
				}

				return _tile;
			}
		}


		public void OnEnable()
		{
			if (Tile.MiniMapDataArray.IsNullOrEmpty())
			{
				Tile.SetupMiniMapData();
				EditorUtility.SetDirty(Tile);
			}
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField("MiniMap用のSpriteを設定してください");
			EditorGUILayout.Space();

			float oldLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 210;

			EditorGUI.BeginChangeCheck();

			foreach (var miniMapData in Tile.MiniMapDataArray)
			{
				miniMapData.sprite = (Sprite)EditorGUILayout.ObjectField(miniMapData.GetTileFlagString(), miniMapData.sprite, typeof(Sprite), false, null);
				miniMapData.color = EditorGUILayout.ColorField(miniMapData.color);
			}

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(Tile);
			}

			EditorGUIUtility.labelWidth = oldLabelWidth;
		}



		[MenuItem("MiniMapTile", menuItem = "Assets/Create/Ling/Tile/MiniMapTile", priority = 1)]
		public static void CreateMiniMapTile()
		{
			string path = EditorUtility.SaveFilePanelInProject("Save MiniMapTile", "New MiniMapTile", "asset", "保存", "Assets");
			if (string.IsNullOrEmpty(path)) return;

			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MiniMapTile>(), path);
		}
	}

#endif
}