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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UtageExtensions;
using Zenject;

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

		/// <summary>
		/// ステージ管理者
		/// </summary>
		public StageManagerMaster StageManager { get; private set; }

		/// <summary>
		/// <see cref="EnemyMaster"/>Repository
		/// </summary>
		public MasterRepository<EnemyMaster> EnemyRepository { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// すべてのマスタデータを読み込む
		/// すでに読み込んでいる場合は削除して読み込み
		/// </summary>
		public async UniTask LoadAllAsync()
		{
			AddLoadTask<ConstMaster>(master => Const = master);
			AddLoadTask<StageManagerMaster>(master => StageManager = master);
			AddLoadRepositoryTask<EnemyMaster>(EnemyRepository);

			// 非同期でTaskを実行し、すべての処理が終わるまで待機
			await UniTask.WhenAll(loadTasks_);

			// todo: 失敗した場合どうするか..

			IsLoaded = true;
		}

		#endregion


		#region private 関数

		/// <summary>
		/// ロード処理リストに突っ込む
		/// </summary>
		private void AddLoadTask<T>(System.Action<T> onSuccess) where T : MasterBase<T> =>
			loadTasks_.Add(LoadAsync<T>(onSuccess));

		private void AddLoadRepositoryTask<T>(MasterRepository<T> repository) where T : MasterBase<T> =>
			loadTasks_.Add(LoadRepositoryAsync<T>(repository));

		/// <summary>
		/// 実際の非同期読み込み処理
		/// </summary>
		private async UniTask LoadAsync<T>(System.Action<T> onSuccess) where T : MasterBase<T>
		{
			var master = await LoadAsyncAtPath<T>($"MasterData/{nameof(T)}");

			onSuccess?.Invoke(master);
		}

		/// <summary>
		/// 指定Masterを検索し、Repositoryにmasterを格納する
		/// </summary>
		private async UniTask LoadRepositoryAsync<T>(MasterRepository<T> repository) where T : MasterBase<T>
		{
			// 指定マスタデータをすべて読み込む
			var filter = $"t:{nameof(T)}";

			foreach (var guid in AssetDatabase.FindAssets(filter, new[] { "MasterData" }))
			{
				var filePath = AssetDatabase.GUIDToAssetPath(guid);
				if (filePath.IsNullOrEmpty()) continue;

				var master = await LoadAsyncAtPath<T>(filePath);

				repository.Add(master);
			}
		}

		private async UniTask<T> LoadAsyncAtPath<T>(string path) where T : MasterBase<T>
		{
			var masterData = Resources.LoadAsync($"MasterData/{nameof(T)}");//.ToUniTask();

			await masterData;

			var master = masterData.asset as T;
			master.Setup();

			return master;
		}

		#endregion
	}
}
