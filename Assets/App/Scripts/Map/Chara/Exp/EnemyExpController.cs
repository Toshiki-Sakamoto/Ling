// 
// EnemyExpController.cs  
// ProductName Ling
//  
// Created by Toshiki Sakamoto on 2021.11.18
// 

using UnityEngine;
using UniRx;
using Zenject;
using System;
using MessagePipe;
using Cysharp.Threading.Tasks;

namespace Ling.Chara.Exp
{
	/// <summary>
	/// 敵の経験値を権利する
	/// </summary>
	[RequireComponent(typeof(ICharaController))]
	public class EnemyExpController : MonoBehaviour, ICharaExpController
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private MasterData.IMasterHolder _masterHolder;
		[Inject] private IAsyncPublisher<Chara.EventLevelUp> _levelupEvent;

		private Subject<int> _subject = new Subject<int>();
		private EnemyControl _enemy;
		private MasterData.Chara.LvTableMaster _lvTableMaster;

		private int _totalExp;      // 累計経験値

		private int _currentLv;     // 現在のレベル
		private int _nextExp;       // 次のレベルに必要な経験値量

		#endregion


		#region プロパティ

		public bool CanLevelUp { get; private set; }

		IObservable<int> ICharaExpController.OnLvUp => _subject;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			_enemy = GetComponent<EnemyControl>();

			var enemyMaster = _enemy.Model.Master;

			// レベルアップ先が有るか
			CanLevelUp = enemyMaster.HasNext;
		}

		void ICharaExpController.Add(int exp)
		{
			_totalExp += exp;

			Apply();
		}

		#endregion


		#region private 関数

		private void Apply()
		{
			if (!CanLevelUp) return;
		}

		#endregion
	}
}