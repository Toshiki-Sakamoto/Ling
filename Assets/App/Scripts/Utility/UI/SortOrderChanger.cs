// 
// SortOrderChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.13
// 

using UnityEngine;
using System.Collections.Generic;
using Ling.Utility.Attribute;

namespace Ling.Utility.UI
{
	/// <summary>
	/// SortOrderの定数と、列挙値を結びつけるための設定を作成する
	/// </summary>
	[CreateAssetMenu(menuName = "Ling/SortOrderSettings")]
	public class SortOrderSettings : ScriptableObject
	{
		#region 定数, class, enum

		[System.Serializable]
		public class ValueData
		{
			[SerializeField] private string _name = default;
			[SerializeField] private int _value = default;

			public string Name => _name;
			public int Value => _value;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private List<ValueData> _data = default;

		#endregion


		#region プロパティ

		public List<ValueData> Data => _data;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public static SortOrderSettings Load()
		{
			var instance = Utility.Editor.AssetHelper.LoadAsset<SortOrderSettings>();
			if (instance == null)
			{
				Utility.Log.Error("指定された保存先にScriptableObjectがありません");
				return null;
			}

			return instance;
		}

		public int Find(string name)
		{
			var data = Data.Find(data => data.Name == name);
			if (data == null)
			{
				Utility.Log.Error($"値が存在しない {name}");
				return 0;
			}

			return data.Value;
		}

		#endregion


		#region private 関数

		#endregion
	}


	/// <summary>
	/// 決められた値にSortOrderを変更する
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class SortOrderChanger : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private static SortOrderSettings sortOrderSettings = default;

		[SerializeField, SortOrderValueAttribute]
		private string _sortOrderName = default;

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
			// 設定がない時生成する
			if (sortOrderSettings == null)
			{
				sortOrderSettings = SortOrderSettings.Load();
				if (sortOrderSettings == null)
				{
					Utility.Log.Error("SortOrderの設定ファイルが存在しない");
					return;
				}
			}

			// Canvasを取得
			var canvas = GetComponent<Canvas>();

			// 値を検索する
			var value = sortOrderSettings.Find(_sortOrderName);
			canvas.sortingOrder = value;
		}

		#endregion
	}
}