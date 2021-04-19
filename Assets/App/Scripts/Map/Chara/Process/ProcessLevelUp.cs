//
// ProcessPostAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.18
//

using System.Collections.Generic;
using Utility.Extensions;

namespace Ling.Chara.Process
{
	/// <summary>
	/// Attackの後処理（経験値等）
	/// </summary>
	public class ProcessLevelUp : Common.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ICharaController _unit;   // 経験値を受けるキャラ
		private List<Chara.ICharaController> _deadChara = new List<ICharaController>(); // 死んだキャラ

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController unit, List<Chara.ICharaController> deadChara)
		{
			_unit = unit;
			_deadChara = deadChara;
		}

		protected override void ProcessStartInternal()
		{
			// 死んだキャラが一体もいない場合、何もしない
			if (_deadChara.Count <= 0)
			{
				ProcessFinish();
				return;
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
