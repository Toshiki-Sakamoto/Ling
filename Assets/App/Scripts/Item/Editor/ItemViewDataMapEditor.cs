//
// ItemDataMapEditor.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.07
//

using UnityEngine;
using Ling.Utility.Extensions;
using System.Linq;
using UnityEditor;
using System;

namespace Ling.Item.Editor
{
	/// <summary>
	/// アイテムの種類と画像を紐付けるDataMapを生成するEditor
	/// </summary>
	[CustomEditor(typeof(ItemViewDataMap))]
	public class ItemViewDataMapEditor : UnityEditor.Editor
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private ItemViewDataMap _target;
		private bool[] _foldings;
		private bool[] _isFolingList;

		#endregion


		#region プロパティ

		private ItemViewDataMap Data => _target ?? (_target = target as ItemViewDataMap);

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void OnEnable()
		{
			var flags = System.Enum.GetValues(typeof(Const.Item.Category));
			var length = flags.Length;

			_isFolingList = new bool[length];

			SetupMapData();
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField("Map用のSpriteを設定してください");
			EditorGUILayout.Space();

			float oldLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 210;

			EditorGUI.BeginChangeCheck();

			ApplyDataMapFolderEditor(0, Data.FoodMap);
			ApplyDataMapFolderEditor(1, Data.BookMap);
			ApplyDataMapFolderEditor(2, Data.Weapon);
			ApplyDataMapFolderEditor(3, Data.Shield);

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(Data);
			}

			EditorGUIUtility.labelWidth = oldLabelWidth;
		}

		#endregion


		#region private 関数


		/// <summary>
		/// リストの折りたたみ処理
		/// </summary>
		void ApplyDataMapFolderEditor<TType>(int index, ItemViewDataMap.SpriteData<TType>[] spriteDataMap) where TType : Enum
		{
			if (_isFolingList[index] = EditorGUILayout.Foldout(_isFolingList[index], typeof(TType).Name))
			{
				EditorGUI.indentLevel++;

				for (int i = 0, length = spriteDataMap.Length; i < length; ++i)
				{
					var spriteData = spriteDataMap[i];
					var sprite = spriteData.Sprite;
					var itemType = spriteData.Type;

					spriteData.SetSprite((Sprite)EditorGUILayout.ObjectField(itemType.ToString(), sprite, typeof(Sprite), false, null));

					GUILayout.Space(20);
				}

				EditorGUI.indentLevel--;
			}
		}

		#endregion


		#region private 関数

		private void SetupMapData()
		{
			Data.SetupDataMap();
		}


		[MenuItem("ItemDataMap", menuItem = "Assets/Create/Ling/Item/ItemDataMap", priority = 1)]
		public static void CreateItemViewDataMap()
		{
			string path = EditorUtility.SaveFilePanelInProject("Save ItemViewDataMap", "New ItemViewDataMap", "asset", "保存", "Assets");
			if (string.IsNullOrEmpty(path)) return;

			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ItemViewDataMap>(), path);
		}

		#endregion
	}
}
