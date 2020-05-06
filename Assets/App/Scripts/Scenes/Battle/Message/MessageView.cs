// 
// BattleMesageView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.06
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;
using UniRx;
using Ling.Utility;

namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _windowRoot = null;
		[SerializeField] private Transform _contentsRoot = null;
		[SerializeField] private MessageItemView _textItem = null;
		[SerializeField] private int _textNum = 5;
		[SerializeField] private int _textItemHeight = 50;
		[SerializeField] private float _topPadding = 0.0f;
		[SerializeField] private float _itemSpace = 0.0f;

		[Inject] private IEventManager _eventManager = null;

		private Queue<MessageItemView> _textItemQueue = new Queue<MessageItemView>();
		private Queue<MessageItemView> _activeTextItemQueue = new Queue<MessageItemView>();
		private MessageItemView _activeTextItem = null;
		private Queue<string> _textQueue = new Queue<string>();
		private bool _canNextTextShow = false;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			_textItem.gameObject.SetActive(false);

			// Textを作成
			for (int i = 0; i < _textNum; ++i)
			{
				var instance = Instantiate<MessageItemView>(_textItem, _contentsRoot);
				instance.OnTextShowEnd = 
					() => 
					{
						// テキスト表示終了時
						// 次に進める
						_canNextTextShow = true;

						ShowTextIfNeeded();
					};

				_textItemQueue.Enqueue(instance);
			}

			_eventManager.Add<EventMessageText>(this, 
				ev_ => 
				{
					SetText(ev_.text);
				});

			_canNextTextShow = true;
		}

		public void SetText(string text)
		{
			_textQueue.Enqueue(text);

			ShowTextIfNeeded();
		}

		#endregion


		#region private 関数


		private void ShowTextIfNeeded()
		{
			// 表示することができるか
			if (!_canNextTextShow) return;
			if (_textQueue.Count <= 0) return;

			// 最大数出ている場合上にずらす
			var upAnimationObservable = Observable.FromCoroutine(() => PlayAnimationActiveTextItem());

			upAnimationObservable.Subscribe(_ =>
				{
					var text = _textQueue.Dequeue();

					// 使用キューに追加
					_activeTextItem = _textItemQueue.Dequeue();
					_activeTextItemQueue.Enqueue(_activeTextItem);

					_activeTextItem.SetText(text);

					AdjustItemsPosition();
				});

			_canNextTextShow = false;
		}


		private IEnumerator PlayAnimationActiveTextItem()
		{
			// 最大数表示されていない場合はアニメーションしない
			if (_activeTextItemQueue.Count < _textNum)
			{
				yield break;
			}
			
			foreach (var item in _activeTextItemQueue)
			{
				item.UpperAnimation(_textItemHeight + _itemSpace);
			}

			foreach (var item in _activeTextItemQueue)
			{
				if (item.IsPlayAnimation) yield return null;
			}

			AdjustItemsPosition();

			// 一番上のアイテムを未使用キューに戻す
			var dequeueItem = _activeTextItemQueue.Dequeue();
			dequeueItem.gameObject.SetActive(false);

			_textItemQueue.Enqueue(dequeueItem);
		}

		/// <summary>
		/// アイテムの座標を調整する
		/// </summary>
		private void AdjustItemsPosition()
		{
			float posY = _topPadding;

			foreach(var item in _activeTextItemQueue)
			{
				item.SetPositionY(posY);

				// 差分込みで足し合わせる
				posY -= _textItemHeight + _itemSpace;
			}
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}