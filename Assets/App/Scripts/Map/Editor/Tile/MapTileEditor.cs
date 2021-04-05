// 
// MiniMapTileEditor.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using UnityEngine;
using Ling.Utility.Extensions;
using System.Linq;

using Ling.Map.Tile;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ling.Map.Editor.Tile
{
	/// <summary>
	/// 
	/// </summary>
#if UNITY_EDITOR
	[CustomEditor(typeof(MapTile))]
	public class MapTileEditor : UnityEditor.Editor
	{
		private MapTile _tile = null;
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


		private MapTile Tile
		{
			get
			{
				if (_tile == null)
				{
					_tile = target as MapTile;
				}

				return _tile;
			}
		}

		public void OnEnable()
		{
			_foldings = new bool[Tile.SpriteData.Length];

			if (ShouldSetupMapData())
			{
				Tile.SetupMapData();

				EditorUtility.SetDirty(Tile);
			}
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField("Map用のSpriteを設定してください");
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
			Tile.TileColor = EditorGUILayout.ColorField("マップタイルの色", Tile.TileColor);

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(Tile);
			}

			EditorGUIUtility.labelWidth = oldLabelWidth;
		}

		private bool ShouldSetupMapData()
		{
			if (Tile.SpriteData.IsNullOrEmpty()) return true;
			if (Tile.SpriteData.Any(spriteData_ => spriteData_ == null)) return true;
			if (Tile.SpriteData.Any(spriteData_ => spriteData_.Sprites.IsNullOrEmpty())) return true;
			return false;
		}



		[MenuItem("MapTile", menuItem = "Assets/Create/Ling/Tile/MapTile", priority = 1)]
		public static void CreateMiniMapTile()
		{
			string path = EditorUtility.SaveFilePanelInProject("Save MapTile", "New MapTile", "asset", "保存", "Assets");
			if (string.IsNullOrEmpty(path)) return;

			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MapTile>(), path);
		}
	}

#endif
}