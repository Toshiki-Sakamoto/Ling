//
// MasterManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using Cysharp.Threading.Tasks;
using Ling.Chara;
using Ling.MasterData.Chara;
using Ling.MasterData.Stage;
using Ling.MasterData.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Ling;
using Zenject;
using Ling.MasterData.Repository;
using Ling.MasterData.Repository.Item;
using UniRx;

namespace Ling.MasterData
{
	/// <summary>
	/// マスタデータ管理者
	/// </summary>
	public class MasterManager : Utility.MonoSingleton<MasterManager>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private List<UniTask> loadTasks_ = new List<UniTask>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 読み込み済みの場合true
		/// </summary>
		public bool IsLoaded { get; private set; }

		/// <summary>
		/// 定数
		/// </summary>
		public ConstMaster Const { get; private set; }

		public EnemyRepository EnemyRepository { get; } = new EnemyRepository();

		public StageRepository StageRepository { get; } = new StageRepository();

		public BookRepository BookRepository { get; } = new BookRepository();

		public FoodRepository FoodRepository { get; } = new FoodRepository();


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// すべてのマスタデータを読み込む
		/// すでに読み込んでいる場合は削除して読み込み
		/// </summary>
		public IObservable<Unit> LoadAll()
		{
			AddLoadTask<ConstMaster>(master => Const = master);
			AddLoadRepositoryTask<EnemyMaster>(EnemyRepository);
			AddLoadRepositoryTask<StageMaster>(StageRepository);
			AddLoadRepositoryTask<BookMaster>(BookRepository);
			AddLoadRepositoryTask<FoodMaster>(FoodRepository);

			// 非同期でTaskを実行し、すべての処理が終わるまで待機
			return UniTask.WhenAll(loadTasks_)
				.ToObservable()
				.Select(_ => 
					{
						Utility.EventManager.SafeTrigger(new MasterLoadedEvent { Manager = this });
						
						return new UniRx.Unit();
					});
		}

		#endregion


		#region private 関数

		/// <summary>
		/// ロード処理リストに突っ込む
		/// </summary>
		private void AddLoadTask<T>(System.Action<T> onSuccess) where T : MasterDataBase =>
			loadTasks_.Add(LoadAsync<T>(onSuccess));

		private void AddLoadRepositoryTask<T>(MasterRepository<T> repository) where T : MasterDataBase =>
			loadTasks_.Add(LoadRepositoryAsync<T>(repository));

		/// <summary>
		/// 実際の非同期読み込み処理
		/// </summary>
		private async UniTask LoadAsync<T>(System.Action<T> onSuccess) where T : MasterDataBase
		{
			var master = await LoadAsyncAtPath<T>($"MasterData/{typeof(T).Name}");

			onSuccess?.Invoke(master);
		}

		/// <summary>
		/// 指定Masterを検索し、Repositoryにmasterを格納する
		/// </summary>
		private async UniTask LoadRepositoryAsync<T>(MasterRepository<T> repository) where T : MasterDataBase
		{
			// 指定マスタデータをすべて読み込む
			foreach (var guid in AssetDatabase.FindAssets($"t:{typeof(T).Name}"))
			{
				var filePath = AssetDatabase.GUIDToAssetPath(guid);
				if (string.IsNullOrEmpty(filePath)) continue;

				// Resourcesファイルパス以下にする
				filePath = Regex.Replace(filePath, ".*/Resources/(.*).asset", "$1");

				var master = await LoadAsyncAtPath<T>(filePath);

				repository.Add(master);
			}
		}

		private async UniTask<T> LoadAsyncAtPath<T>(string path) where T : MasterDataBase
		{
			var masterData = Resources.LoadAsync(path);//.ToUniTask();

			await masterData;

			var master = masterData.asset as T;
			master.Setup();

			return master;
		}

		#endregion
	}
}
