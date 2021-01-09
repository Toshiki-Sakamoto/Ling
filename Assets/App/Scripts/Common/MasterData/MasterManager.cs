//
// MasterManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UniRx;

namespace Ling.Common.MasterData
{
	public interface IMasterManager
	{
		/// <summary>
		/// マスタデータ読み込み済みの場合true
		/// </summary>
		bool IsLoaded { get; }

		/// <summary>
		/// 全マスタデータの読み込みを行う
		/// </summary>
		IObservable<AsyncUnit> LoadAll();

		
		TMaster GetMaster<TMaster>() where TMaster : MasterDataBase;

		TRepository GetRepository<TRepository>();
	}


	/// <summary>
	/// マスタデータ管理者
	/// </summary>
	public abstract class MasterManager : MonoBehaviour, IMasterManager
    {
		#region 定数, class, enum


		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<Type, IMasterRepository> _repositories = new Dictionary<Type, IMasterRepository>();
		private Dictionary<Type, MasterDataBase> _masters = new Dictionary<Type, MasterDataBase>();
		protected List<UniTask> _loadTasks = new List<UniTask>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 読み込み済みの場合true
		/// </summary>
		public bool IsLoaded { get; private set; }



		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public abstract IObservable<AsyncUnit> LoadAll();

		public TMaster GetMaster<TMaster>()
			where TMaster : MasterDataBase
		{
			if (!_masters.TryGetValue(typeof(TMaster), out var result))
			{
				// todo: エラー処理
			}

			return (TMaster)result;
		}

		/// <summary>
		/// 読み込み済みのリポジトリを取得する
		/// </summary>
		public TRepository GetRepository<TRepository>()
		{
			if (!_repositories.TryGetValue(typeof(TRepository), out var result))
			{
				// todo: エラー処理
			}

			return (TRepository)result;
		}


		#endregion


		#region private 関数

		/// <summary>
		/// ロード処理リストに突っ込む
		/// </summary>
		protected void AddLoadTask<TMaster>() where TMaster : MasterDataBase
		{
			_loadTasks.Add(LoadAsync<TMaster>(master => 
				{
					// todo: 以前のデータが存在する場合、削除するかClearするだけにするか決めること
					_masters.Add(typeof(TMaster), master);
				}));
		}

		protected void AddLoadRepositoryTask<TMaster, TRepository>() 
			where TMaster : MasterDataBase
			where TRepository : MasterRepository<TMaster>, new()
		{
			// todo: 以前のデータが存在する場合、削除するかClearするだけにするか決めること
			var repository = new TRepository();
			_repositories.Add(typeof(TRepository), repository);
			
			_loadTasks.Add(LoadRepositoryAsync<TMaster>(repository));
		}

		/// <summary>
		/// 実際の非同期読み込み処理
		/// </summary>
		protected async UniTask LoadAsync<T>(System.Action<T> onSuccess) where T : MasterDataBase
		{
			var master = await LoadAsyncAtPath<T>($"MasterData/{typeof(T).Name}");

			onSuccess?.Invoke(master);
		}

		/// <summary>
		/// 指定Masterを検索し、Repositoryにmasterを格納する
		/// </summary>
		protected async UniTask LoadRepositoryAsync<T>(MasterRepository<T> repository) where T : MasterDataBase
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

		protected async UniTask<T> LoadAsyncAtPath<T>(string path) where T : MasterDataBase
		{
			var masterData = Resources.LoadAsync(path);//.ToUniTask();

			await masterData;

			var master = masterData.asset as T;
			master.Setup();

			return master;
		}

		/// <summary>
		/// すべての読み込みが終了したときに呼び出す
		/// </summary>
		protected void LoadFinished()
		{
			Utility.EventManager.SafeTrigger(new MasterLoadedEvent {  });
		}

		#endregion
	}
}
