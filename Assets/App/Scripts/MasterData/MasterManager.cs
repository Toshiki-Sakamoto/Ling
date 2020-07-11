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
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
		/// マップ関連
		/// </summary>
		public Stage.MapMaster[] MapMasters { get; private set; }

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
			AddLoadTask<ConstMaster>(master_ => Const = master_);


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

		/// <summary>
		/// 実際の非同期読み込み処理
		/// </summary>
		private async UniTask LoadAsync<T>(System.Action<T> onSuccess) where T : MasterBase<T>
		{
			var masterData = Resources.LoadAsync($"MasterData/{typeof(T).Name}");//.ToUniTask();

			await masterData;

			onSuccess(masterData.asset as T);
		}

		#endregion
	}
}
