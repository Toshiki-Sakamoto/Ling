//
// MasterManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using Cysharp.Threading.Tasks;
using System;

namespace Utility.MasterData
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
		UniTask LoadAll();


		TGaemData GetData<TGaemData>() where TGaemData : Utility.GameData.GameDataBase;

		TRepository GetRepository<TRepository>();
	}


	/// <summary>
	/// マスタデータ管理者
	/// </summary>
	public abstract class MasterManager : Utility.GameData.GameDataManager, IMasterManager
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

		protected void LoadFinished()
		{
			LoadFinished<MasterLoadedEvent>();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
