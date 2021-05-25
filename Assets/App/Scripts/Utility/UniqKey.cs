//
// UUID.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.25
//

using UnityEngine;
using System;

namespace Utility
{
	/// <summary>
	/// シリアライズ可能なUUID
	/// </summary>
	[System.Serializable]
	public class UniqKey : ISerializationCallbackReceiver
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private string _value;

		private Guid _guid;

		#endregion


		#region プロパティ

		public string Value => _value;

		#endregion


		#region コンストラクタ, デストラクタ

		// Note: EasySaveで保存できるのはデフォルトコンストラクタのみを持っているクラスなのでSetupというメソッドで初期化するようにしてる
		public void Setup(Guid guid)
		{
			_guid = guid;
			_value = _guid.ToString();

			Utility.Log.Print($"UniqKey コンストラクタ {_value}");
		}

		#endregion


		#region public, protected 関数

		public static UniqKey Create()
		{
			var instance = new UniqKey();
			instance.Setup(Guid.NewGuid());

			return instance;
		}

		public void OnAfterDeserialize()
		{
			try 
			{
				_guid = Guid.Parse(_value);
			} 
			catch 
			{
				_guid = Guid.Empty;
				Debug.LogWarning($"Attempted to parse invalid GUID string '{_value}'. GUID will set to System.Guid.Empty");
			}
		}
 
		public void OnBeforeSerialize() 
		{
			_value = _guid.ToString();
		}

		public override bool Equals(object obj)
		{
			return obj is UniqKey guid && _guid.Equals(guid._guid);
		}

		public override int GetHashCode()
		{
			return _guid.GetHashCode();
		}

		public override string ToString() => _guid.ToString();


		public static bool operator ==(UniqKey a, UniqKey b) => a._guid == b._guid;
		public static bool operator !=(UniqKey a, UniqKey b) => !(a == b);

		#endregion


		#region private 関数

		#endregion
	}
}
