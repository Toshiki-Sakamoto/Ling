// 
// MoveInputView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.01.11
// 

using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Operators;
using Ling.Common;
using System.Collections.Generic;

namespace Ling.InputUI
{
	/// <summary>
	/// 移動入力を処理するView
	/// </summary>
	public class MoveInputProvider : MonoBehaviour, IInputProvider
    {
		#region 定数, class, enum

		[System.Serializable]
		public class KeyData
		{
			[SerializeField] private Common.UI.ButtonEx _button = default;
			[SerializeField] private List<KeyCode> _keyCodes = default;

			public void Setup()
			{
			}
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private List<KeyData> _keyData = default;
		[SerializeField] private Button _dirSwitchButton = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// キーが押されている間
		/// </summary>
		public bool GetKey(KeyCode keyCode)
		{
			return false;
		}

		/// <summary>
		/// キーが押されたとき一回だけ
		/// </summary>
		public bool GetKeyDown(KeyCode keyCode)
		{
			return false;
		}

		/// <summary>
		/// キーが離されたとき一回だけ
		/// </summary>
		public bool GetKeyUp(KeyCode keyCode)
		{
			return false;
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
			/*
			_leftButton.onClick.AsObservable().Subscribe(_ => 
				{
					Debug.Log("Click");
				});*/
		}


		#endregion
	}
}