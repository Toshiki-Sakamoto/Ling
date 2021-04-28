//
// BattlePhaseBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// バトルシーンのPhaseベースクラス
	/// </summary>
	public class BattlePhaseBase : Utility.Phase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] protected BattleScene _scene;
		[Inject] protected BattleModel _model;
		[Inject] protected BattleManager _battleManager;
		[Inject] protected Utility.IEventManager _eventManager;

		#endregion


		#region プロパティ

		public BattleScene Scene => _scene;
		public EventHolder EventHolder => _battleManager.EventHolder;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected TProcess AttachProcess<TProcess>() where TProcess : Common.ProcessBase
		{
			return _scene.AttachProcess<TProcess>();			
		}

		#endregion


		#region private 関数

		#endregion
	}
}
