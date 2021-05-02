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
				});

			// 経験値量を表示
			Utility.Log.Print($"経験値ゲット {_chara.Name} exp:{_exp}");

			_eventManager.Trigger(new Chara.EventAddedExp { Chara = _chara, Exp = _exp });

			_chara.ExpController.Add(_exp);

			disposable.Dispose();


			ProcessFinish();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
