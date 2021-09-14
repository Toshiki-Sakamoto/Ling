//
// BattlePhaseLoad.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using Cysharp.Threading.Tasks;
using MessagePipe;
using Ling.Map;

using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseLoad : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private PoolManager _poolManager;
		[Inject] private Map.MapManager _mapManager = null;
		[Inject] private Chara.CharaManager _charaManager = null;
		[Inject] private Common.Scene.IExSceneManager _sceneManager = default;

		[Inject] private IPublisher<EventSpawnMapObject> _eventSpawn;

		private bool _isFinish = false;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		public override void PhaseInit()
		{
		}

		public override void PhaseStart()
		{
			// タイルのプールを作成する
			// とりあえず最大の数作ってみる
			LoadAsync().Forget();
		}

		public override void PhaseUpdate()
		{
			if (!_isFinish) return;

			Change(Phase.FloorSetup);
		}

		public override void PhaseStop()
		{
			_isFinish = false;
		}

		#endregion


		#region private 関数

		private async UniTask LoadAsync()
		{
			// マップ情報の構築
			await _mapManager.Setup(_model.StageMaster, _model.IsResume);

			if (_model.IsResume)
			{
				await _charaManager.ResumeAsync();
			}
			else
			{
				// キャラクタのセットアップ処理
				await _charaManager.InitializeAsync();
			}

			_charaManager.SetStageMaster(_model.StageMaster);

			// プレイヤーにMap情報を初期座標を設定
			var mapControl = Scene.MapControl;

			var player = _charaManager.Player;
			mapControl.SetChara(player);

			if (_model.IsResume)
			{
				Scene.DeployObjectToMap(isResume: true);
			}
			else
			{
				var builder = _mapManager.CurrentMapData.Builder;
				var playerPos = builder.GetPlayerInitPosition();
				
				player.Model.InitPos(playerPos);

				// プレイヤーが生成されたイベントを投げる
				_eventSpawn.Publish(new EventSpawnMapObject 
					{ 
						Flag = Const.TileFlag.Player, 
						MapLevel = player.Model.MapLevel, 
						followObj = player.gameObject 
					});
				
				// 初期マップの敵を生成する
				await _charaManager.BuildEnemyGroupAsync(1, _mapManager.FindGroundTilemap(1));
				await _charaManager.BuildEnemyGroupAsync(2, _mapManager.FindGroundTilemap(2));
				
				// 敵をマップに配置する
				Scene.DeployObjectToMap(isResume: false);
			}

			// Player ステイタスUIを表示する
			// todo: シーンの依存関係に紐付けたい
//			await _sceneManager.AddSceneAsync<Status.StatusScene>(Common.Scene.SceneID.Status, argument: null);


			_isFinish = true;
		}

		#endregion
	}
}
