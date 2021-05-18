//
// IGameDataLoader.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Utility.GameData
{
	/// <summary>
	/// 読み込みInterface
	/// </summary>
	public interface IGameDataLoader
	{
		UniTask<T> LoadAssetAsync<T>(string key);
		UniTask<IList<T>> LoadAssetsAsync<T>(string key);
	}
}
