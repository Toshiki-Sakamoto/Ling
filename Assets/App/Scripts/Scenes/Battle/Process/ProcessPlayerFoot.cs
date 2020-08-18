//
// ProcessPlayerFoot.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.09
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling.Map;
using Ling.Const;
using Ling.Map.TileDataMapExtension;

using Zenject;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 足元確認
	/// </summary>
	public class ProcessPlayerFoot : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager = null;
		[Inject] private MapManager _mapManager = null;

		private Chara.PlayerView _player;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数
		
		/// <summary>
		/// 前のプロセスが終了したときに呼び出される
		/// </summary>
		public override void ProcessStart()
		{
			_player = _charaManager.PlayerView;

			var tileDataMap = _mapManager.CurrentTileDataMap;
			var tileFlag = tileDataMap.GetTileFlag(_player.CellPos);

			// 下り階段
			if (tileFlag.HasStepDown())
			{
				SetNext<ProcessConfirmStepDown>();
			}

			ProcessFinish();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
