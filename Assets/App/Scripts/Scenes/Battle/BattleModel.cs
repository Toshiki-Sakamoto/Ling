//
// BattleModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using Ling.MasterData.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class BattleModel : MonoBehaviour
	{
		#region 定数, class, enum

		public class Param
		{
			public StageMaster stageMaster; // バトルステージ情報
		}

		#endregion


		#region public, protected 変数

		public Param param;

		#endregion


		#region private 変数

		private Param _param;

		#endregion


		#region プロパティ

		/// <summary>
		/// バトルステージ情報
		/// </summary>
		public StageMaster StageMaster => param.stageMaster;

		/// <summary>
		/// 現在のレベル
		/// </summary>
		public int Level { get; private set; } = 1;

		/// <summary>
		/// 次のフェーズ移動の予約
		/// </summary>
		public Phase NextPhaseMoveReservation { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Param param)
		{
			this.param = param;
		}

		public void NextLevel()
		{
			++Level;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
