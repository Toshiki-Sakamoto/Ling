//
// EnemyModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.12
//

using Ling.MasterData.Chara;

namespace Ling.Chara
{
	/// <summary>
	/// EnemyModel
	/// </summary>
	public class EnemyModel : CharaModel
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public override string Name => Master.Name;

		public EnemyMaster Master { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetMaster(EnemyMaster master)
		{
			Master = master;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
