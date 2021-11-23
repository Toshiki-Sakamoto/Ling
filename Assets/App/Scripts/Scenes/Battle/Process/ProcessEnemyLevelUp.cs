//
// ProcessAddExp.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.11.21
//

using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 敵のレベルアップ処理
	/// </summary>
	public class ProcessEnemyLevelUp : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Utility.IEventManager _eventManager = default;

		private Chara.EnemyControl _enemy;
		private int _exp;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara)
		{
			_enemy = chara as Chara.EnemyControl;
		}

		protected override void ProcessStartInternal()
		{
			PlayLevelUp().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask PlayLevelUp()
		{
			// レベルアップ処理を行う

			ProcessFinish();
		}

		#endregion
	}
}
