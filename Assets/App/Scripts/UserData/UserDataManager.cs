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

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override IObservable<AsyncUnit> LoadAll()
		{
			// 非同期でTaskを実行し、すべての処理が終わるまで待機
			return UniTask.WhenAll(_loadTasks)
				.ToObservable()
				.Do(_ =>
					{
						LoadFinished();
					});
		}

		#endregion


		#region private 関数

		#endregion
	}
}
