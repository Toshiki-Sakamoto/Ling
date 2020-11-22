//
// ItemViewDataMap.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.07
//


using UnityEngine;
using System;

namespace Ling.Item
{
	/// <summary>
	/// Itemの種類に応じて見た目を変えるための情報を持つクラス
	/// </summary>
	[System.Serializable]
	public class ItemViewDataMap : ScriptableObject
    {
		#region 定数, class, enum

		/// <summary>
		/// Spriteと紐付ける
		/// </summary>
		[System.Serializable]
		public class SpriteData<TType> where TType : Enum
		{
			[SerializeField] private Sprite _sprite = default;
			[SerializeField] private TType _type = default;

			public Sprite Sprite => _sprite;
			public TType Type => _type;

			public Sprite SetSprite(Sprite sprite) =>
				_sprite = sprite;

			public TType SetType(TType type) =>
				_type = type;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private SpriteData<Const.Item.Food>[] _foodMap = default;
		[SerializeField] private SpriteData<Const.Item.Book>[] _bookMap = default;
		[SerializeField] private SpriteData<Const.Item.Weapon>[] _weaponMap = default;
		[SerializeField] private SpriteData<Const.Item.Shield>[] _shieldMap = default;

		#endregion


		#region プロパティ

		public SpriteData<Const.Item.Food>[] FoodMap => _foodMap;
		public SpriteData<Const.Item.Book>[] BookMap => _bookMap;
		public SpriteData<Const.Item.Weapon>[] Weapon => _weaponMap;
		public SpriteData<Const.Item.Shield>[] Shield => _shieldMap;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetupDataMap()
		{
			_foodMap = SetupDataMapInternal(_foodMap);
			_bookMap = SetupDataMapInternal(_bookMap);
			_weaponMap = SetupDataMapInternal(_weaponMap);
			_shieldMap = SetupDataMapInternal(_shieldMap);
		}

		#endregion


		#region private 関数

		private SpriteData<TType>[] CreateDataMap<TType>(SpriteData<TType>[] spriteDataMap) where TType : Enum
		{
			var flags = System.Enum.GetValues(typeof(TType));
			if (spriteDataMap != null)
			{
				// サイズ同じなら何もしない
				if (spriteDataMap.Length == flags.Length)
				{
					return spriteDataMap;
				}
			}

			var dataMap = new SpriteData<TType>[flags.Length];

			int count = 0;
			foreach (TType flag in flags)
			{
				var data = new SpriteData<TType>();
				data.SetType(flag);

				dataMap[count++] = data;
			}

			// サイズ変更により中身を差し替える
			var length = Mathf.Min(spriteDataMap.Length, flags.Length);
			for (int i = 0; i < length; ++i)
			{
				var sprite = spriteDataMap[i].Sprite;
				dataMap[i].SetSprite(sprite);
			}

			return dataMap;
		}

		private SpriteData<TType>[] SetupDataMapInternal<TType>(SpriteData<TType>[] spriteDataMap) where TType : Enum
		{
			spriteDataMap = CreateDataMap<TType>(spriteDataMap);
			
			for (int i = 0; i < spriteDataMap.Length; ++i)
			{
				if (spriteDataMap[i] == null)
				{
					spriteDataMap[i] = new SpriteData<TType>();
				}
			}

			return spriteDataMap;
		}


		#endregion
	}
}
