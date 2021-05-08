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
using Ling.MasterData.Equipment;
using Ling.MasterData.Repository;
using Ling.MasterData.Repository.Item;
using Ling.MasterData.Repository.Player;
using Ling.MasterData.Repository.Equipment;
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
		PlayerLvTableRepository PlayerLvTableRepository { get; }

		ItemRepositoryContainer ItemRespositoryContainer { get; }
		EquipRepositoryContainer EquipRepositoryContainer { get; }
	}

	/// <summary>
	/// プロジェクト固有のマスターマネージャー
	/// </summary>
	public class MasterManager : Utility.MasterData.MasterManager, IMasterHolder
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
		public ConstMaster Const => GetData<ConstMaster>();

		public EnemyRepository EnemyRepository => GetRepository<EnemyRepository>();
		public StageRepository StageRepository => GetRepository<StageRepository>();
		public BookRepository BookRepository => GetRepository<BookRepository>();
		public FoodRepository FoodRepository => GetRepository<FoodRepository>();
		public PlayerLvTableRepository PlayerLvTableRepository => GetRepository<PlayerLvTableRepository>();
		public WeaponRepository WeaponRepository => GetRepository<WeaponRepository>();
		public ShieldRepository ShieldRepository => GetRepository<ShieldRepository>();

		public ItemRepositoryContainer ItemRespositoryContainer { get; } = new ItemRepositoryContainer();
		public EquipRepositoryContainer EquipRepositoryContainer { get; } = new EquipRepositoryContainer();


		#endregion


		#region public, protected 関数

		/// <summary>
		/// すべてのマスタデータを読み込む
		/// すでに読み込んでいる場合は削除して読み込み
		/// </summary>
		public async override UniTask LoadAll()
		{
			AddLoadTask<ConstMaster>("ConstMaster");
			AddLoadRepositoryTask<EnemyMaster, EnemyRepository>("EnemyMaster");
			AddLoadRepositoryTask<StageMaster, StageRepository>("StageMaster");
			AddLoadRepositoryTask<BookMaster, ItemMaster, BookRepository>("ItemBookMaster");
			AddLoadRepositoryTask<FoodMaster, ItemMaster, FoodRepository>("ItemFoodMaster");
			AddLoadRepositoryTask<LvTableMaster, PlayerLvTableRepository>("PlayerLvTableMaster");
			AddLoadRepositoryTask<WeaponMaster, EquipmentMaster, WeaponRepository>("WeaponMaster");
			AddLoadRepositoryTask<ShieldMaster, EquipmentMaster, ShieldRepository>("ShieldMaster");

			// 非同期でTaskを実行し、すべての処理が終わるまで待機
			await UniTask.WhenAll(_loadTasks);

			ItemRespositoryContainer.Update(BookRepository, FoodRepository);
			EquipRepositoryContainer.Update(WeaponRepository, ShieldRepository);

			LoadFinished();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}