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
using Ling.Common.Tile;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ling.Common.Editor.Tile
{
	/// <summary>
	/// 
	/// </summary>
#if UNITY_EDITOR
	[CustomEditor(typeof(MiniMapTile))]
	public class MiniMapTileEditor : UnityEditor.Editor
	{
		private MiniMapTile _tile = null;
		private bool[] _foldings;
		private bool _isFolingList;
		private Sprite _allSetSprite;

		public static readonly string[] SpriteTile = 
			{
				"Filled",
				"Three Sides",
				"Two Sides and One Corner",
				"Two Adjacent Sides",
				"Two Opposite Sides",
				"One Side and Two Corners",
				"One Side and One Lower Conrner",
				"One Side and One Upper Conrner",
				"One Side",
				"Four Corners",
				"Three Corners",
				"Two Adjacent Corners",
				"Two Oppsite Corners",
				"One Corner",
				"Empty",
			};


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
			if (Tile.SpriteData.IsNullOrEmpty())
			{
				Tile.SetupMiniMapData();

				EditorUtility.SetDirty(Tile);
			}

			_foldings = new bool[Tile.SpriteData.Length];
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField("MiniMap用のSpriteを設定してください");
			EditorGUILayout.Space();

			float oldLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 210;

			EditorGUI.BeginChangeCheck();

			// リストの折りたたみ展開
			if (_isFolingList = EditorGUILayout.Foldout(_isFolingList, "List"))
			{
				// TileFlagの数だけリスト展開
				for (int i = 0; i < Tile.SpriteData.Length; ++i)
				{
					EditorGUI.indentLevel++;

					var spriteData = Tile.SpriteData[i];

					// 要素一つ
					if (_foldings[i] = EditorGUILayout.Foldout(_foldings[i], spriteData.GetTileFlagString()))
					{
						// Sprite一つ一つ
						for (int j = 0; j < spriteData.Sprites.Length; ++j)
						{
							spriteData.Sprites[j] = (Sprite)EditorGUILayout.ObjectField(SpriteTile[j], spriteData.Sprites[j], typeof(Sprite), false, null);
						}

						// 全てを同じものに設定する
						GUILayout.Space(20);

						_allSetSprite = (Sprite)EditorGUILayout.ObjectField("一度に設定", _allSetSprite, typeof(Sprite), false, null);

						if (GUILayout.Button("同じものを一度に設定"))
						{
							for (int j = 0; j < spriteData.Sprites.Length; ++j)
							{
								spriteData.Sprites[j] = _allSetSprite;
							}
						}
					}

					EditorGUI.indentLevel--;
				}
			}

			GUILayout.Space(20);

			// 色
			Tile.TileColor = EditorGUILayout.ColorField("ミニマップタイルの色", Tile.TileColor);

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