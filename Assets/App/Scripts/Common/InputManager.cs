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

namespace Ling.Common
{
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

		#endregion
	}
}
