// 
// MasterManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.01.07
// 

using UnityEngine;
using System.Collections.Generic;
using Ling.MasterData.Chara;
using Ling.MasterData.Stage;
using Ling.MasterData.Item;
using Ling.MasterData.Repository;
using Ling.MasterData.Repository.Item;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using System;

namespace Ling.MasterData
{
	/// <summary>
	/// 各種マスタデータを保持、操作する
	/// </summary>
	public interface IMasterHolder
	{
		ConstMaster Const { get; }

		EnemyRepository EnemyRepository { get; }
		StageRepository StageRepository { get; }
		BookRepository BookRepository { get; }
		FoodRepository FoodRepository { get; }
	}

	/// <summary>
	/// プロジェクト固有のマスターマネージャー
	/// </summary>
	public class MasterManager : Common.MasterData.MasterManager, IMasterHolder
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// 定数
		/// </summary>
		public ConstMaster Const => GetMaster<ConstMaster>();

		public EnemyRepository EnemyRepository => GetRepository<EnemyRepository>();
		public StageRepository StageRepository => GetRepository<StageRepository>();
		public BookRepository BookRepository => GetRepository<BookRepository>();
		public FoodRepository FoodRepository => GetRepository<FoodRepository>();

		//public ItemRepositoryContainer ItemRepositoryContainer { get; } = new ItemRepositoryContainer();


		#endregion


		#region public, protected 関数

		/// <summary>
		/// すべてのマスタデータを読み込む
		/// すでに読み込んでいる場合は削除して読み込み
		/// </summary>
		public override IObservable<AsyncUnit> LoadAll()
		{
			AddLoadTask<ConstMaster>();
			AddLoadRepositoryTask<EnemyMaster, EnemyRepository>();
			AddLoadRepositoryTask<StageMaster, StageRepository>();
			AddLoadRepositoryTask<BookMaster, BookRepository>();
			AddLoadRepositoryTask<FoodMaster, FoodRepository>();

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


		#region MonoBegaviour

		#endregion
	}
}