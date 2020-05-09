// 
// BattleMessageItemView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.06
// 
using Ling.Adv.Engine.Command;
using Ling.Adv.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageItemView : MonoBehaviour
	{
		#region 定数, class, enum

		private static readonly int UpperTrigger = Animator.StringToHash("Upper");

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private AdvText _text = null;
		[SerializeField] private Animator _animator = null;

		private RectTransform _rectTransform = null;
		private float _messageDisplaySpeed = 0f;

		#endregion


		#region プロパティ

		public Adv.Document Document { get; private set; } = new Adv.Document();

		public bool IsPlayAnimation { get; private set; }

		public System.Action OnTextShowEnd { get; set; }

		#endregion


		#region public, protected 関数

		public void SetText(string text)
		{
			gameObject.SetActive(true);

			Document.Load(text);

			_text.SetDocument(Document);
			_text.SetLengthOfView(0);
			_text.text = text;

			int length = 0;

			Observable
				.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(_messageDisplaySpeed))
				.Select(value_ => 
					{
						// 一文字すすめる
						Document.NextTextIndex();

						_text.SetLengthOfView(++length);

						return Document.IsEnd;
					})
				.TakeWhile(isEnd_ => isEnd_ ? false : true)
				.Subscribe(_ => {}, onCompleted: () =>
					{
						OnTextShowEnd?.Invoke();
					});
		}

		/// <summary>
		/// アニメーション
		/// </summary>
		public void UpperAnimation(float addPosY, float itemUpperAnimationTime)
		{
			IEnumerator MovePosY()
			{
				float startPosY = _rectTransform.anchoredPosition.y;
				float time = 0.0f;

				while (time < itemUpperAnimationTime)
				{
					var rario = Mathf.Min(time / itemUpperAnimationTime, 1.0f);
					var posY = addPosY * rario + startPosY;

					_rectTransform.anchoredPosition = new Vector2(0.0f, posY);

					yield return null;

					time += Time.deltaTime;
				}

				_rectTransform.anchoredPosition = new Vector2(0.0f, startPosY + addPosY);

				yield return null;

				OnUpperAnimationEnd();
			}

			StartCoroutine(MovePosY());

//			_animator.SetTrigger(UpperTrigger);
			IsPlayAnimation = true;
		}

		public void OnUpperAnimationEnd()
		{
			IsPlayAnimation = false;
		}

		public void SetPositionY(float y)
		{
			// Anchor指定してあるのでこっちで移動
			_rectTransform.anchoredPosition = new Vector2(0.0f, y);
		}

		public void ChangeTextDisplaySpeed(float speed)
		{
			_messageDisplaySpeed = speed;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		#endregion
	}
}