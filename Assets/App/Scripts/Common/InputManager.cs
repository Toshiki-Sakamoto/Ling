using System.Linq;
//
// InputManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.12.31
//

using System.Collections.Generic;
using Ling.Utility.Extensions;
using UnityEngine;
using Ling.Utility;
using UniRx;
using UniRx.Triggers;

namespace Ling.Common.Input
{
	/// <summary>
	/// 押されている間のキー入力を受け付ける
	/// </summary>
	public interface IInputPressedListener
	{
		void OnInputPressed(KeyCode keyCode);
	}

	/// <summary>
	/// キーが押された瞬間のキー入力を受け付ける
	/// </summary>
	public interface IInputDownListener
	{
		void OnInputDown(KeyCode keyCode);
	}

	/// <summary>
	/// キーが離された瞬間の入力を受け付ける
	/// </summary>
	public interface IInputUpListener
	{
		void OnInputUp(KeyCode keyCode);
	}

	/// <summary>
	/// Pressed, Down, Up すべての通知を受け取る
	/// </summary>
	public interface IInputAllListener : IInputPressedListener, IInputDownListener, IInputUpListener
	{
	}


	/// <summary>
	/// 押されている間フレームごとに投げられるイベント
	/// </summary>
	public class InputPressedEvent
	{
	}

	/// <summary>
	/// キーが押された瞬間投げられるイベント
	/// </summary>
	public class InputDownEvent
	{

	}

	/// <summary>
	/// キーが離された瞬間投げられるイベント
	/// </summary>
	public class InputUpEvent
	{

	}


	/// <summary>
	/// キー入力を管理する
	/// </summary>
	public class InputManager : MonoBehaviour, IInputProvider
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private List<(GameObject, IInputProvider)> _inputProviders = new List<(GameObject, IInputProvider)>();
		private Dictionary<KeyCode, List<GameObject>> _keyEventDataDict = new Dictionary<KeyCode, List<GameObject>>();
		private Dictionary<IInputPressedListener, List<KeyCode>> _keyPressedListeners = new Dictionary<IInputPressedListener, List<KeyCode>>();
		private Dictionary<IInputDownListener, List<KeyCode>> _keyDownListeners = new Dictionary<IInputDownListener, List<KeyCode>>();
		private Dictionary<IInputUpListener, List<KeyCode>> _keyUpListeners = new Dictionary<IInputUpListener, List<KeyCode>>();
		

		#endregion


		#region プロパティ

		/// <summary>
		/// キー入力が有効状態の時true
		/// </summary>
		public bool Enabled { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// キー入力を有効/無効化する
		/// </summary>
		public void SetEnabled(bool enable)
		{
			Enabled = enable;
		}

		public void AddProvider(IInputProvider provider, GameObject owner = null)
		{
			if (provider == (IInputProvider)this) 
			{
				throw new System.Exception("自分自身を追加しようとしました");
			}

			_inputProviders.Add((owner, provider));

			// ownerがあればそのownerが削除されるときに一緒にRemoveする
			if (owner != null)
			{
				DestoryCallbacker.AddOnDestoryCallback(owner, 
					_ => 
					{
						RemoveProvider(provider);
					});
			}
		}

		public void RemoveProvider(IInputProvider provider)
		{
			_inputProviders.RemoveAllWithRef((ref (GameObject, IInputProvider) item) => item.Item2 == provider);
		}

		/// <summary>
		/// キーが押されているとき通知を受ける
		/// </summary>
		public void AddPressedListener(KeyCode keyCode, IInputPressedListener listener) =>
			AddListenerInternal(keyCode, listener, _keyPressedListeners);

		/// <summary>
		/// キーが押されたとき一回だけ通知を受ける
		/// </summary>
		public void AddDownListener(KeyCode keyCode, IInputDownListener listener) =>
			AddListenerInternal(keyCode, listener, _keyDownListeners);

		/// <summary>
		/// キーが離されたとき通知を受ける
		/// </summary>
		public void AddUpListener(KeyCode keyCode, IInputUpListener listener) =>
			AddListenerInternal(keyCode, listener, _keyUpListeners);

		public void AddListener(KeyCode keyCode, IInputAllListener listener)
		{
			AddPressedListener(keyCode, listener);
			AddDownListener(keyCode, listener);
			AddUpListener(keyCode, listener);
		}

		public void RemovePressedListener(IInputPressedListener listener) =>
			RemoveListenerInternal(listener, _keyPressedListeners);

		public void RemoveDownListener(IInputDownListener listener) =>
			RemoveListenerInternal(listener, _keyDownListeners);

		public void RemoveUpListener(IInputUpListener listener) =>
			RemoveListenerInternal(listener, _keyUpListeners);

		public void RemoveListener(IInputAllListener listener)
		{
			RemovePressedListener(listener);
			RemoveDownListener(listener);
			RemoveUpListener(listener);
		}


		public bool GetKey(KeyCode keyCode)
		{
			if (!Enabled) return false;

			foreach (var provider in _inputProviders.FastReverse())
			{
				if (provider.Item2.GetKey(keyCode))
				{
					return true;
				}
			}

			return false;
		}

		public bool GetKeyDown(KeyCode keyCode)
		{
			if (!Enabled) return false;

			foreach (var provider in _inputProviders.FastReverse())
			{
				if (provider.Item2.GetKeyDown(keyCode))
				{
					return true;
				}
			}

			return false;
		}

		public bool GetKeyUp(KeyCode keyCode)
		{
			if (!Enabled) return false;

			foreach (var provider in _inputProviders.FastReverse())
			{
				if (provider.Item2.GetKeyUp(keyCode))
				{
					return true;
				}
			}

			return false;
		}

		#endregion


		#region private 関数

		
		private void AddListenerInternal<TListener>(KeyCode keyCode, TListener listener, Dictionary<TListener, List<KeyCode>> listeners)
		{
			if (listener == null)
			{
				throw new System.Exception("listenerを指定してください");
			}

			if (!listeners.TryGetValue(listener, out var list))
			{
				list = new List<KeyCode>();
				listeners.Add(listener, list);
			}

			if (list.Exists(keyCode_ => keyCode_ == keyCode))
			{
				throw new System.Exception($"同じKeyCodeの二重登録は認められていません {keyCode}");
			}

			list.Add(keyCode);
		}

		private void RemoveListenerInternal<TListener>(TListener listener, Dictionary<TListener, List<KeyCode>> listeners)
		{
			listeners.Remove(listener);
		}

		private void Awake()
		{
			// tood: Dictを一つ一つ分解してイテレーション回せるようにできないかな
			this.UpdateAsObservable()
				.Where(_ => _keyPressedListeners.Count > 0)
				.Do(_ =>
				{
					foreach (var keyValue in _keyPressedListeners)
					{
						var listener = keyValue.Key;
						foreach (var keyCode in keyValue.Value)
						{
							if (GetKey(keyCode))
							{
								listener.OnInputPressed(keyCode);
							}
						}
					}
				});

			this.UpdateAsObservable()
				.Where(_ => _keyDownListeners.Count > 0)
				.Do(_ =>
				{
					foreach (var keyValue in _keyDownListeners)
					{
						var listener = keyValue.Key;
						foreach (var keyCode in keyValue.Value)
						{
							if (GetKeyDown(keyCode))
							{
								listener.OnInputDown(keyCode);
							}
						}
					}
				});

			this.UpdateAsObservable()
				.Where(_ => _keyUpListeners.Count > 0)
				.Do(_ =>
				{
					foreach (var keyValue in _keyUpListeners)
					{
						var listener = keyValue.Key;
						foreach (var keyCode in keyValue.Value)
						{
							if (GetKeyUp(keyCode))
							{
								listener.OnInputUp(keyCode);
							}
						}
					}
				});
		}

		#endregion
	}
}
