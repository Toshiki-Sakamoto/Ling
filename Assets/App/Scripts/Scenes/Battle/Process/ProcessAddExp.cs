//
// ProcessAddExp.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.02
//

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 経験値加算処理
	/// </summary>
	public class ProcessAddExp : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ICharaController _chara;
		private int _exp;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public ProcessAddExp(Chara.ICharaController chara, int exp)
		{
			_chara = chara;
			_exp = exp;
		}

		#endregion


		#region public, protected 関数

		public void Add(int exp)
		{
			_exp += exp;
		}

		protected override void ProcessStartInternal()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
