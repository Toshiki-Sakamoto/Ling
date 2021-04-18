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
	public class BattlePhaseBase : Utility.Phase<Phase>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] protected BattleScene _scene;
		[Inject] protected BattleModel _model;
		[Inject] protected GameManager _gameManager;
		[Inject] protected Utility.IEventManager _eventManager;
		[Inject] protected Common.ProcessManager _processManager;

		#endregion


		#region プロパティ

		public BattleScene Scene => _scene;
		public EventHolder EventHolder => _gameManager.EventHolder;

		#endregion


		#region コンストラクタ, デストラクタ

		public void Awake()
		{
			_gameManager = GameManager.Instance;

			AwakeInternal();
		}

		#endregion


		#region public, protected 関数

		protected virtual void AwakeInternal() { }

		#endregion


		#region private 関数

		#endregion
	}
}
