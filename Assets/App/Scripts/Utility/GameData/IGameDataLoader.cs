//
// IGameDataLoader.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.GameData
{
	/// <summary>
	/// 読み込みInterface
	/// </summary>
	public interface IGameDataLoader
	{
		UniTask<T> LoadAssetAsync<T>(string key)
			where T : class;

		UniTask<IList<T>> LoadAssetsAsync<T>(string key)
			where T : class;
	}

	public interface IGameDataCreator
	{
		T Create<T>() where T : class;
	}

	/// <summary>
	/// 保存の役目
	/// </summary>
	public interface IGameDataSaver
	{
		T Save<T>(string key, T value)
			where T : class;
	}

	/// <summary>
	/// セーブ可能であること
	/// </summary>
	public interface IGameDataSavable
	{
		string SaveDataKey { get; set; }

		bool Save(IGameDataSaver saver);
	}

}
