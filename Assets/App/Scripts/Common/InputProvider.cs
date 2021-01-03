//
// IInputProvider.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.12.30
//

using UnityEngine;

namespace Ling.Common
{
	/// <summary>
	/// インプット処理を担う
	/// </summary>
	public interface IInputProvider
    {
		/// <summary>
		/// キーが押されている間
		/// </summary>
		bool GetKey(KeyCode keyCode);

		/// <summary>
		/// キーが押されたとき一回だけ
		/// </summary>
		bool GetKeyDown(KeyCode keyCode);

		/// <summary>
		/// キーが離されたとき一回だけ
		/// </summary>
		bool GetKeyUp(KeyCode keyCode);
	}

#if UNITY_EDITOR
	/// <summary>
	/// キーボード入力
	/// </summary>
	public class KeyInputProvider : IInputProvider
	{
		/// <summary>
		/// キーが押されている間
		/// </summary>
		public bool GetKey(KeyCode keyCode) =>
			Input.GetKey(keyCode);

		/// <summary>
		/// キーが押されたとき一回だけ
		/// </summary>
		public bool GetKeyDown(KeyCode keyCode) =>
			Input.GetKeyDown(keyCode);

		/// <summary>
		/// キーが離されたとき一回だけ
		/// </summary>
		public bool GetKeyUp(KeyCode keyCode) =>
			Input.GetKeyUp(keyCode);
	}
#endif
}
