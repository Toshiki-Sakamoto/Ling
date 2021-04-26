//
// IGameDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using System.Collections.Generic;
using Zenject;
using Utility.DebugConfig;
using Utility.Extensions;

namespace Utility.GameData
{
#if DEBUG

	public abstract class RepositoryDebugMenu : Utility.DebugConfig.DebugMenuItem.Data
	{
		// 保存ファイルの削除
		private DebugButtonItem.Data _fileRemovebutton;

		/// <summary>
		/// デバッグ読み込みのOn/Off
		/// </summary>
		public DebugCheckItem.Data EnableDebugMode { get; private set; }


		public RepositoryDebugMenu(string title)
			: base(title)
		{
			_fileRemovebutton = new DebugButtonItem.Data("ファイル削除", 
				() => 
				{
					RemoveFile();
				});

			EnableDebugMode = new DebugCheckItem.Data("デバッグ読み込み");

			Add(_fileRemovebutton);
			Add(EnableDebugMode);
		}

		public abstract void RemoveFile();
	}

#endif

	public interface IGameDataRepository
	{
		void Initialize();

		void Clear();
	}

	/// <summary>
	/// User/Master データ管理リポジトリベース
	/// </summary>
	public abstract class GameDataRepository<T> : IGameDataRepository, 
		Utility.Repository.IRepository<T>
		where T : IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public List<T> Entities { get; } = new List<T>();


#if DEBUG
		protected abstract bool EnableDebugMode { get; }
#endif

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Add(IEnumerable<T> entities)
		{
			if (entities == null) return;

			Entities.AddRange(entities);
		}

		public abstract void Initialize();

		public void Clear() =>
			Entities.Clear();
			
		/// <summary>
		/// 読み込み終了時に呼び出される
		/// </summary>
		public void AddFinished()
		{
#if DEBUG
			// 読み込み終了時デバッグモードがONの場合、リストを削除してメソッドを呼び出す
			if (EnableDebugMode)
			{
				Clear();

				DebugAddFinished();
			}
#endif
		}

#if DEBUG
		protected virtual void DebugAddFinished() {}
#endif

		#endregion


		#region private 関数

		#endregion
	}
}
