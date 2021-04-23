//
// UserDataManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

using Ling.UserData.Item;
using Ling.UserData.Repository;

using Zenject;

namespace Ling.UserData
{
	/// <summary>
	/// 各種ユーザーデータのRepositoryを返す
	/// </summary>
	public interface IUserDataHolder
	{

	}

	/// <summary>
	/// ユーザーごとに保持されるデータ
	/// </summary>
	public class UserDataManager : Utility.UserData.UserDataManager, IUserDataHolder
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

#if DEBUG
		[Inject] private Utility.DebugConfig.DebugRootMenuData _rootDebug;
#endif

		#endregion


		#region プロパティ

		public ItemUserDataRepository ItemRepository => GetRepository<ItemUserDataRepository>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public async override UniTask LoadAll()
		{
			AddLoadRepositoryTask<ItemUserData, ItemUserDataRepository>("");

			// 非同期でTaskを実行し、すべての処理が終わるまで待機
			await UniTask.WhenAll(_loadTasks);

			LoadFinished();
		}

		#endregion


		#region private 関数

#if DEBUG
		private void Awake()
		{
			_rootDebug.CreateAndAddItem<_Debug.DebugUserData>();
		}
#endif

		#endregion
	}
}
