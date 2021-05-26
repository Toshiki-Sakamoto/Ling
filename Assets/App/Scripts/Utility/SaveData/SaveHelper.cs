//
// SaveHelper.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.19
//

using UnityEngine;
using System.Collections.Generic;

namespace Utility.SaveData
{
	/// <summary>
	/// 保存、読み込みの手助けをする
	/// </summary>
	public interface ISaveDataHelper
	{
		void Save<T>(string key, T value);
		void Save<T>(string path, string key, T value);

		T Load<T>(string key);
		T Load<T>(string path, string key);

		bool Exists(string key);
		bool Exists(string path, string key);
	}

	/// <summary>
	/// ヘルパクラス
	/// </summary>
	public class SaveHelper : MonoBehaviour, ISaveDataHelper
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private ES3Settings _settings;
		private Dictionary<string, ES3Settings> _settingDict = new Dictionary<string, ES3Settings>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		
		void ISaveDataHelper.Save<T>(string key, T value)
		{
			ES3.Save<T>(key, value, _settings);
		}
		void ISaveDataHelper.Save<T>(string path, string key, T value)
		{
			ES3.Save<T>(key, value, GetOrCreateSettings(path));
		}

		T ISaveDataHelper.Load<T>(string key)
		{
			var helper = (ISaveDataHelper)this;
			return helper.Load<T>(null, key);
		}
		T ISaveDataHelper.Load<T>(string path, string key)
		{
			T result = default(T);

			if (string.IsNullOrEmpty(path))
			{
				result = ES3.Load<T>(key);
			}
			else
			{
				result = ES3.Load<T>(key, GetOrCreateSettings(path));
			}

			return result;
		}

		bool ISaveDataHelper.Exists(string key)
		{
			return ES3.KeyExists(key);
		}
		bool ISaveDataHelper.Exists(string path, string key)
		{
			return ES3.KeyExists(key, GetOrCreateSettings(path));
		}

		#endregion


		#region private 関数

		private ES3Settings GetOrCreateSettings(string path)
		{
			if (_settingDict.TryGetValue(path, out var settings))
			{
				return settings;
			}

			// ない場合は作成
			settings = new ES3Settings(path: path);
			_settingDict.Add(path, settings);
			
			return settings;
		}

		private void Awake()
		{
			_settings = new ES3Settings();
		}

		#endregion
	}
}
