//
// SortOrderSettings.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.13
//

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ling.Utility.AssetBundle;

namespace Ling.Utility.UI
{
	/// <summary>
	/// SortOrderの定数と、列挙値を結びつけるための設定を作成する
	/// </summary>
	[CreateAssetMenu(menuName = "Ling/SortOrderSettings")]
	public class SortOrderSettings : ScriptableObject
	{
		#region 定数, class, enum

		public const string Address = "UI_SortOrderSettings";

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

		private static SortOrderSettings settings = null;

		[SerializeField] private List<ValueData> _data = default;

		#endregion


		#region プロパティ

		public static SortOrderSettings Settings
		{
			get
			{
#if UNITY_EDITOR
				// Editorモードのときはローカルファイルを読み込む
				if (settings == null)
				{
					settings = Utility.AssetBundle.AddressableHelper.LoadAsset<SortOrderSettings>(address: Address);
				}
#endif

				return settings;
			}
		}

		public List<ValueData> Data => _data;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public static async UniTask LoadAsync(Component owner)
		{
			settings = await AssetBundleManager.Instance.LoadAssetAsync<SortOrderSettings>(Address, owner);
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
}
