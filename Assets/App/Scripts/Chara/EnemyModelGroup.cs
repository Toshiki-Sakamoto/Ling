//
// EnemyModelGroup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using Cysharp.Threading.Tasks;
using Ling.MasterData.Stage;
using Ling.Scenes.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// 
	/// </summary>
	public class EnemyModelGroup : ModelGroupBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private MapMaster _mapMaster;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetMapMaster(MapMaster mapMaster)
		{
			_mapMaster = mapMaster;
		}

		public CharaModel CreateEnemyModel()
		{
			var mapEnemyData = _mapMaster.GetRandomEnemyDataFromPopRate();
			var enemyModel = EnemyFactory.Create(mapEnemyData);

			Models.Add(enemyModel);

			return enemyModel;
		}

		protected override async UniTask SetupAsyncInternal()
		{
			Models.Clear();

			for (int i = 0, size = _mapMaster.InitCreateNum.GetRandomValue(); i < size; ++i)
			{
				CreateEnemyModel();
			}
		}

		/// <summary>
		/// 指定座標にキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(Vector2Int pos)
		{
			return Models.Exists(model => model.Pos == pos);
		}

		public override void OnDestroy()
		{
			Models.Clear();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
