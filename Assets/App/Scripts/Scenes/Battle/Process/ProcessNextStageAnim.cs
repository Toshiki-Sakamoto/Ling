//
// ProcessNextStageAnim.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.28
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 階層を移動するときのアニメーション
	/// </summary>
	public class ProcessNextStageAnim : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private GameManager _gameManager = null;
		[Inject] private Utility.IEventManager _eventManager = null;
		[Inject] private MapManager _mapManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Start()
		{
			MoveAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTaskVoid MoveAsync()
		{
			var mapControl = _mapManager.MapControl;

			await mapControl.MoveUpAsync();
			await PlayerMoveAsync();

			ProcessFinish();
		}

		private async UniTask PlayerMoveAsync()
		{
			await transform.DOJump(new Vector3(0f, 0.0f, 0.0f), 5.0f, 1, 1.0f);
		}

		#endregion
	}
}
