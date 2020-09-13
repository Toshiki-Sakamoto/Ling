//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Ling.AI.Attack
{
	using CharaMaster = Ling.MasterData.Chara;

	/// <summary>
	/// 攻撃AIのベースクラス
	/// </summary>
	public abstract class AIBase : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager = default;
		[Inject] private Chara.CharaManager _charaManager = default;

		private CharaMaster.AttackAIData _masterAIData;
		private Chara.ICharaController _unit;
		private Map.TileDataMap _tileDataMap;
		private Map.RoomData _roomData;


		#endregion


		#region プロパティ

		/// <summary>
		/// 行動できるか
		/// </summary>
		public bool IsActable { get; private set; }

		public Map.TileDataMap TileDataMap
		{
			get 
			{
				if (_tileDataMap != null) return _tileDataMap;

				_tileDataMap = _mapManager.MapControl.FindTileDataMap(_unit.Model.MapLevel);
				return _tileDataMap;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(CharaMaster.AttackAIData attackAIData)
		{
			_masterAIData = attackAIData;
		}

		/// <summary>
		/// 思考処理
		/// 非同期にしているのは、逐次処理を戻すことで１フレーム内の思考時間最大数超えていた場合次フレームに回すため
		/// </summary>
		public virtual async UniTask ExecuteAsync(Chara.ICharaController unit, Ling.Utility.Async.WorkTimeAwaiter timeAwaiter)
		{
			_unit = unit;
			_tileDataMap = null;
			_roomData = null;

			await ExexuteInternalAsync(timeAwaiter);
		}

		public void Reset()
		{
			IsActable = false;
		}


		protected virtual UniTask ExexuteInternalAsync(Ling.Utility.Async.WorkTimeAwaiter timeAwaiter) =>
			default(UniTask);

		#endregion


		#region private 関数


		#endregion
	}
}
