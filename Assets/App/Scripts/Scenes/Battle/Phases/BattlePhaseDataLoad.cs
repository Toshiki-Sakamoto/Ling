//
// BattlePhaseDataLoad.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.29
//

using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;
using System.Threading;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 中断データの読み込み
	/// </summary>
	public class BattlePhaseDataLoad : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Utility.IEventManager _eventManager = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 非同期
		/// </summary>
		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			// 必要なデータを順次読み込み
			var loadEvent = new Utility.SaveData.EventLoadCall();
			_eventManager.Trigger(loadEvent);

			foreach (var task in loadEvent.LoadTasks)
			{
				await task;
			}
			
			// Phase開始
			Change(Phase.Start);
		}
		
		#endregion


		#region private 関数

		#endregion
	}
}
