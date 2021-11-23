//
// ProcessAddExp.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.02
//

using UniRx;
using Zenject;

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

		[Inject] private Utility.IEventManager _eventManager = default;

		private Chara.ICharaController _chara;
		private int _exp;

		#endregion


		#region プロパティ

		public Chara.ICharaController Chara => _chara;

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara, int exp)
		{
			_chara = chara;
			_exp = exp;
		}
		
		public void Add(int exp)
		{
			_exp += exp;
		}

		protected override void ProcessStartInternal()
		{
			var disposable = _chara.ExpController.OnLvUp
				.Subscribe(lv => 
				{
					// Lv Up
					Utility.Log.Print($"LvUp! {_chara.Name} {lv}");

					_eventManager.Trigger(new Chara.EventLevelUp { Chara = _chara, Lv = lv });

					// 敵の場合敵レベルアップ演出を入れる
					if (_chara.Model.CharaType == Ling.Chara.CharaType.Enemy)
					{
						var levelUpProcess = SetNext<ProcessEnemyLevelUp>();
						levelUpProcess.Setup(_chara);
					}
				});

			// 経験値量を表示
			Utility.Log.Print($"経験値ゲット {_chara.Name} exp:{_exp}");

			_chara.ExpController.Add(_exp);

			disposable.Dispose();

			if (_chara.Model.CharaType == Ling.Chara.CharaType.Player)
			{
				_eventManager.Trigger(new Chara.EventAddedExp { Chara = _chara, Exp = _exp });
			}

			ProcessFinish();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
