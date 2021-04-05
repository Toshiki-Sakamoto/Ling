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
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
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
			float GetItemSize(int index);

			/// <summary>
			/// セルのGameObjectを返す
			/// </summary>
			GameObject GetItemObj(int index);

			/// <summary>
			/// スクロールアイテムの更新を行う
			/// </summary>
			void ScrollItemUpdate(int index, GameObject obj);
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
			Vertical,   // 縦
			Horizontal, // 横
		}

		/// <summary>
		/// Item情報
		/// </summary>
		public class ItemData
		{
			public GameObject obj;
			public GameObject objOrigin;
			public RectTransform rectTransform;
			public float size;
			public int index;

			public static ItemData Create(GameObject newObject, GameObject origin, int index, float size)
			{
				return new ItemData()
				{
					obj = newObject,
					objOrigin = origin,
					rectTransform = newObject.GetComponent<RectTransform>(),
					size = size,
					index = index
				};
			}

			public void RemoveObject()
			{
				if (obj == null) return;

				GameObject.Destroy(obj);
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
		[SerializeField] private bool _autoInitItemInstantiate = default;   // 初期化とき、自動でアイテムの生成を行う

		[Inject] DiContainer _container = null;

		private ScrollRect _scrollRect;
		private List<ItemData> _items = new List<ItemData>();
		private List<float> _positionCaches = new List<float>();
		private IContentDataProvider _dataProvider = null;
		private Dictionary<GameObject, List<ItemData>> _itemDataCaches = new Dictionary<GameObject, List<ItemData>>();
		private RectTransform _rectTransform;
		private RectTransform _contentRectTransform;
		private int _currentItemNo = 0;

		#endregion


		#region プロパティ

		/// <summary>
		/// ScrollRect RectTransform
		/// </summary>
		protected RectTransform RectTransform
		{
			get
			{
				if (_rectTransform == null)
				{
					_rectTransform = GetComponent<RectTransform>();
				}

				return _rectTransform;
			}
		}

		/// <summary>
		/// ScrollView Content RectTransform
		/// </summary>
		protected RectTransform ContentRectTransform
		{
			get
			{
				// C# 8
				//return _rectTransform ??= GetComponent<RectTransform>(); 
				if (_contentRectTransform == null)
				{
					//_rectTransform = GetComponent<RectTransform>();
					_contentRectTransform = ScrollRect.content.GetComponent<RectTransform>();
				}

				return _contentRectTransform;
			}
		}

		protected ScrollRect ScrollRect
		{
			get
			{
				if (_scrollRect != null) return _scrollRect;

				_scrollRect = GetComponentInParent<ScrollRect>();
				if (_scrollRect == null)
				{
					Utility.Log.Error("ScrollRectが見つからない");
					return null;
				}

				_scrollRect.horizontal = _direction == Direction.Horizontal;
				_scrollRect.vertical = _direction == Direction.Vertical;

				return _scrollRect;
			}
		}

		/// <summary>
		/// 現在のContentの左上の座標
		/// </summary>
		private float AnchoredPosition
		{
			get
			{
				return _direction == Direction.Vertical ?
					-ContentRectTransform.anchoredPosition.y :
					ContentRectTransform.anchoredPosition.x;
			}
		}

		/// <summary>
		/// ScrollRect描画サイズ
		/// </summary>
		private float ScrollRectSize
		{
			get
			{
				return _direction == Direction.Vertical ?
					RectTransform.sizeDelta.y :
					RectTransform.sizeDelta.x;
			}
		}

		/// <summary>
		/// ScrollRect Conentサイズ
		/// </summary>
		private float ContentSize
		{
			get
			{
				return _direction == Direction.Vertical ?
					ContentRectTransform.sizeDelta.y :
					ContentRectTransform.sizeDelta.x;
			}
		}

		private float BottomPosition
		{
			get
			{
				return AnchoredPosition;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Initialize(IContentDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
			_currentItemNo = 0;

			if (_autoInitItemInstantiate)
			{
				// 初期化時に何個生成できるかを自動で判別する
				var rectSizeDelta = RectTransform.sizeDelta;

				int currentItemIndex = 0;
				float totalSize = 0;

				do
				{
					var itemScale = GetItemScale(currentItemIndex);
					if (itemScale <= 0)
					{
						break;
					}

					++currentItemIndex;

					if (currentItemIndex >= _dataProvider.DataCount)
					{
						break;
					}

					totalSize += itemScale + _spacing;

					var contentSize = _direction == Direction.Vertical ?
							rectSizeDelta.y :
							rectSizeDelta.x;

					if (totalSize >= contentSize)
					{
						break;
					}

				} while (true);

				_instantateItemCount = currentItemIndex;
			}

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

				while (_items.Count > 0)
				{
					RemoveItem(_items[0]);
				}

				for (var i = 0; i < _instantateItemCount; ++i)
				{
					_items.Add(GetItem(/*_currentItemNo + */i, null));
				}
			}

			var rectTransform = ContentRectTransform;
			var sizeDelta = rectTransform.sizeDelta;

			Vector2 newContentSize;

			if (_direction == Direction.Vertical)
			{
				sizeDelta.y = _padding.top + _padding.bottom;

				for (var i = 0; i < dataProvider.DataCount; ++i)
				{
					sizeDelta.y += GetItemScale(i) + _spacing;
				}

				newContentSize = new Vector2(sizeDelta.x, Mathf.Max(sizeDelta.y, RectTransform.sizeDelta.y));
			}
			else
			{
				sizeDelta.x = _padding.left + _padding.right;

				for (var i = 0; i < dataProvider.DataCount; ++i)
				{
					sizeDelta.x += GetItemScale(i) + _spacing;
				}

				newContentSize = new Vector2(Mathf.Max(sizeDelta.x, RectTransform.sizeDelta.x), sizeDelta.y);
			}

			rectTransform.sizeDelta = newContentSize;
		}

		/// <summary>
		/// リフレッシュ処理をかけて初期化からやり直す
		/// </summary>
		public void Refresh(bool needRemoveCache = true)
		{
			// キャッシュを削除するか
			if (needRemoveCache)
			{
				foreach (var itemDataList in _itemDataCaches)
				{
					foreach (var itemData in itemDataList.Value)
					{
						itemData.RemoveObject();
					}
				}

				_itemDataCaches.Clear();
			}

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

			return _dataProvider.GetItemSize(Math.Max(0, Mathf.Min(index, _dataProvider.DataCount - 1)));
		}

		private ItemData GetItem(int index, ItemData recyclableItem)
		{
			if (_dataProvider == null || index < 0 || _dataProvider.DataCount <= index)
			{
				recyclableItem?.obj.SetActive(false);

				return recyclableItem;
			}

			// 表示するGameObject
			var gameObj = _dataProvider.GetItemObj(index);
			if (gameObj == null)
			{
				return null;
			}

			// キャッシュに登録されているか
			List<ItemData> objCaches;
			if (!_itemDataCaches.TryGetValue(gameObj, out objCaches))
			{
				objCaches = new List<ItemData>();
				_itemDataCaches.Add(gameObj, objCaches);
			}

			if (recyclableItem != null)
			{
				if (gameObj != recyclableItem.objOrigin)
				{
					var caches = _itemDataCaches[recyclableItem.objOrigin];
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
					recyclableItem.index = index;   // 参照Index値を更新する
					objCaches.RemoveAt(0);
				}
				else
				{
					// なければ生成
					// todo: ここは生成方法を選択できるようにしておくと良いかも
					var newObject = _container.InstantiatePrefab(gameObj, ScrollRect.content);
					recyclableItem = ItemData.Create(newObject, gameObj, index, _dataProvider.GetItemSize(index));
				}
			}

			if (recyclableItem.rectTransform.parent != ScrollRect.content)
			{
				recyclableItem.rectTransform.SetParent(ScrollRect.content, false);
			}

			// 座標の設定
			recyclableItem.rectTransform.anchoredPosition = GetVectorPositionByCache(index);
			recyclableItem.obj.SetActive(true);

			// 更新を伝える
			_dataProvider.ScrollItemUpdate(index, recyclableItem.obj);

			return recyclableItem;
		}

		/// <summary>
		/// 各Itemの座標を保存しておく。
		/// 指定されたIndexの座標を取得する
		/// </summary>
		private float GetPositionByCache(int index)
		{
			for (var i = _positionCaches.Count; i <= index; ++i)
			{
				_positionCaches.Add(i == 0 ? (_direction == Direction.Vertical ? _padding.top : _padding.left) : (_positionCaches[i - 1] + GetItemScale(i - 1) + _spacing));
			}

			return _positionCaches[index];
		}

		/// <summary>
		/// 指定した場所のアイテムの座標をVector2で取得する
		/// </summary>
		private Vector2 GetVectorPositionByCache(int index)
		{
			if (index < 0) return Vector2.zero;

			return _direction == Direction.Vertical ?
				new Vector2(0, -GetPositionByCache(index)) :
				new Vector2(GetPositionByCache(index), 0);
		}

		/// <summary>
		/// 指定したアイテムを削除する
		/// </summary>
		private void RemoveItem(ItemData itemData)
		{
			itemData.obj.SetActive(false);
			_items.Remove(itemData);

			var caches = _itemDataCaches[itemData.objOrigin];
			caches.Add(itemData);
		}



		#endregion

		protected void Update()
		{
			if (_dataProvider == null) return;
			if (_items.Count <= 1) return;

			// Yは下がプラスで上がマイナスになる

			// 下にスクロール : AnchordPositionはマイナスになっていく
			do
			{
				// 一番上のアイテムが見えなくなってれば削除
				var item = _items[0];
				var size = item.size;

				var position = GetPositionByCache(item.index);
				if (position + size + AnchoredPosition >= 0)
				{
					break;
				}

				// 先頭要素はもう見えないので削除する
				RemoveItem(item);

				++_currentItemNo;

			} while (true);

			do
			{
				// 一番下にアイテムが追加できるならば追加する
				// 追加できるものがなければ終わり
				if (_currentItemNo + _items.Count >= _dataProvider.DataCount)
				{
					break;
				}

				var item = _items.Last();
				var position = GetPositionByCache(item.index);
				if (AnchoredPosition + position + item.size > ScrollRectSize)
				{
					break;
				}

				_items.Add(GetItem(_currentItemNo + _items.Count, null));

			} while (true);

			// 上にスクロール : AnchordPositionはプラスになっていく
			do
			{
				// 一番下のアイテムが見えなくなってれば削除
				var item = _items.Last();
				var position = GetPositionByCache(item.index);

				if (position + AnchoredPosition <= ScrollRectSize)
				{
					break;
				}

				RemoveItem(item);

			} while (true);

			do
			{
				// 一番上にアイテムが追加できるならば追加する
				// 追加できるものがなければ終わり
				if (_currentItemNo <= 0)
				{
					break;
				}

				var item = _items.First();
				var position = GetPositionByCache(item.index);
				if (AnchoredPosition + position <= 0)
				{
					break;
				}

				--_currentItemNo;

				_items.Insert(0, GetItem(_currentItemNo, null));

			} while (true);
		}
	}
}
