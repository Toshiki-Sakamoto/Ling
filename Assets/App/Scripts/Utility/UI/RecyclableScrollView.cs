//
// RecyclableScrollView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.23
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Zenject;

namespace Ling.Utility.UI
{
	/// <summary>
	/// 再利用可能なスクロールビュー
	/// </summary>
	public class RecyclableScrollView : UIBehaviour
    {
		#region 定数, class, enum

		/// <summary>
		/// スクロールに必要なデータを提供する
		/// </summary>
		public interface IContentDataProvider
		{ 
			/// <summary>
			/// データの個数
			/// </summary>
			int DataCount { get; }

			/// <summary>
			/// セルの高さor幅を返す
			/// </summary>
			float GetItemScale(int index);

			/// <summary>
			/// セルのGameObjectを返す
			/// </summary>
			GameObject GetItemObj(int index);

			/// <summary>
			/// データのUpdateをしてもらう
			/// </summary>
			/// <param name="index"></param>
			/// <param name="obj"></param>
			void DataUpdate(int index, GameObject obj);
		}

		/// <summary>
		/// パディング
		/// </summary>
		[System.Serializable]
		public class Padding
		{
			public int top = 0;
			public int right = 0;
			public int bottom = 0;
			public int left = 0;
		}

		/// <summary>
		/// スクロールの向き
		/// </summary>
		public enum Direction
		{
			Vertical,	// 縦
			Horizontal,	// 横
		}

		/// <summary>
		/// Item情報
		/// </summary>
		public class ItemData
		{
			public GameObject Obj { get; private set; }
			public GameObject ObjOrigin { get; private set; }
			public RectTransform RectTrs { get; private set; }

			public static ItemData Create(GameObject origin)
			{
				var newObj = Instantiate(origin);
				return new ItemData() { Obj = newObj, ObjOrigin = origin, RectTrs = newObj.GetComponent<RectTransform>() };
			}
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Padding _padding = null;
		[SerializeField] private int _spacing = 0;
		[SerializeField] private Direction _direction = Direction.Vertical;
		[SerializeField, Range(0, 20)] private int _instantateItemCount = 7;

		private List<ItemData> _items = new List<ItemData>();
		private List<float> _positionCaches = new List<float>();
		private IContentDataProvider _dataProvider = null;
		private Dictionary<GameObject, List<ItemData>> _itemDataCaches = new Dictionary<GameObject, List<ItemData>>();
		private RectTransform _rectTransform;
		private float _diffPreFramePosition = 0f;
		private int _currentItemNo = 0;

		#endregion


		#region プロパティ

		protected RectTransform RectTransform 
		{ 
			get
			{
				// C# 8
				//return _rectTransform ??= GetComponent<RectTransform>(); 
				if (_rectTransform == null)
				{
					_rectTransform = GetComponent<RectTransform>();
				}

				return _rectTransform;
			}
		}

		private float AnchoredPosition
		{
			get
			{
				return _direction == Direction.Vertical ?
					-RectTransform.anchoredPosition.y :
					RectTransform.anchoredPosition.x;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Initialize(IContentDataProvider dataProvider)
		{
			_dataProvider = dataProvider;

			if (_items.Count == 0)
			{
				for (var i = 0; i < _instantateItemCount; ++i)
				{
					_items.Add(GetItem(i, null));
				}
			}
			else
			{
				_positionCaches.Clear();

				for (var i = 0; i < _instantateItemCount; ++i)
				{
					var item = _items[0];
					_items.RemoveAt(0);
					_items.Add(GetItem(_currentItemNo + i, item));
				}
			}

			var rectTransform = RectTransform;
			var delta = rectTransform.sizeDelta;

			if (_direction == Direction.Vertical)
			{
				delta.y = _padding.top + _padding.bottom;

				for (var i = 0; i < dataProvider.DataCount; ++i)
				{
					delta.y += GetItemScale(i) + _spacing;
				}
			}
			else
			{
				delta.x = _padding.left + _padding.right;

				for (var i = 0; i < dataProvider.DataCount; ++i)
				{
					delta.x += GetItemScale(i) + _spacing;
				}
			}

			rectTransform.sizeDelta = delta;
		}

		public void Refresh()
		{
			_itemDataCaches.Clear();

			Initialize(_dataProvider);
		}

		#endregion


		#region private 関数

		private float GetItemScale(int index)
		{
			if (_dataProvider == null || _dataProvider.DataCount == 0)
			{
				return 0;
			}

			return _dataProvider.GetItemScale(Math.Max(0, Mathf.Min(index, _dataProvider.DataCount - 1)));
		}

		private ItemData GetItem(int index, ItemData recyclableItem)
		{
			if (_dataProvider == null || index < 0 || _dataProvider.DataCount <= index)
			{
				recyclableItem?.Obj?.gameObject.SetActive(false);

				return recyclableItem;
			}

			// 表示するGameObject
			var gameObj = _dataProvider.GetItemObj(index);

			// キャッシュに登録されているか
			List<ItemData> objCaches;
			if (!_itemDataCaches.TryGetValue(gameObj, out objCaches))
			{
				objCaches = new List<ItemData>();
				_itemDataCaches.Add(gameObj, objCaches);
			}

			if (recyclableItem != null)
			{
				if (gameObj != recyclableItem.ObjOrigin)
				{
					var caches = _itemDataCaches[recyclableItem.ObjOrigin];
					caches.Add(recyclableItem);

					recyclableItem = null;
				}
			}
			else 
			{
				// キャッシュがあればそれを使用する。なければ新しく生成
				if (objCaches.Count > 0)
				{
					recyclableItem = objCaches[0];
					objCaches.RemoveAt(0);
				}
				else
				{
					// なければ生成
					// todo: ここは生成方法を選択できるようにしておくと良いかも
					recyclableItem = ItemData.Create(gameObj);
				}
			}

			if (recyclableItem.RectTrs.parent != transform)
			{
				recyclableItem.RectTrs.SetParent(transform, false);
			}

			recyclableItem.RectTrs.anchoredPosition = GetPosition(index);
			recyclableItem.Obj.SetActive(true);

			// 更新を伝える
			_dataProvider.DataUpdate(index, recyclableItem.Obj);

			return recyclableItem;
		}

		/// <summary>
		/// 各Itemの座標を保存しておく。
		/// 指定されたIndexの座標を取得する
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private float GetPositionCache(int index)
		{
			for (var i = _positionCaches.Count; i <= index; ++i)
			{
				_positionCaches.Add(i == 0 ? (_direction == Direction.Vertical ? _padding.top : _padding.left) : (_positionCaches[i - 1] + GetItemScale(i - 1) + _spacing));
			}

			return _positionCaches[index];
		}

		private Vector2 GetPosition(int index)
		{
			if (index < 0) return Vector2.zero;

			return _direction == Direction.Vertical ? 
				new Vector2(0, -GetPositionCache(index)) : 
				new Vector2(GetPositionCache(index), 0);
		}

		#endregion



		protected override void Start()
		{
			var scrollRect = GetComponentInParent<ScrollRect>();
			if (scrollRect == null) 
			{
				Utility.Log.Error("ScrollRectが見つからない");
				return;
			}

			scrollRect.horizontal = _direction == Direction.Horizontal;
			scrollRect.vertical = _direction == Direction.Vertical;
			scrollRect.content = RectTransform;
		}

		protected void Update()
		{
			if (_dataProvider == null) return;

			do
			{
				var itemScale = GetItemScale(_currentItemNo);
				if (itemScale <= 0 || AnchoredPosition - _diffPreFramePosition >= -(itemScale - _spacing) * 2)
				{
					break;
				}

				var item = _items[0];
				_items.RemoveAt(0);
				_diffPreFramePosition -= itemScale + _spacing;
				_items.Add(GetItem(_currentItemNo + _instantateItemCount, item));

				++_currentItemNo;

			} while (true);

			do
			{
				var itemScale = GetItemScale(_currentItemNo + _instantateItemCount - 1);
				if (itemScale <= 0 || AnchoredPosition - _diffPreFramePosition <= -(itemScale + _spacing))
				{
					break;
				}

				var item = _items[_items.Count - 1];
				_items.RemoveAt(_items.Count - 1);

				--_currentItemNo;

				_diffPreFramePosition += GetItemScale(_currentItemNo) + _spacing;
				_items.Insert(0, GetItem(_currentItemNo, item));

			} while (true);
		}
	}
}
