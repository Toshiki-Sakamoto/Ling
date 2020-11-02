//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using Ling.Map.TileDataMapExtensions;

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

		[Inject] protected Map.MapManager _mapManager = default;
		[Inject] protected Chara.CharaManager _charaManager = default;

		protected CharaMaster.AttackAIData _masterAIData;
		protected Chara.ICharaController _unit;
		protected Map.TileDataMap _tileDataMap;
		protected Map.RoomData _roomData;


		#endregion


		#region プロパティ

		/// <summary>
		/// 行動できるか
		/// </summary>
		public bool CanActable { get; protected set; }

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

			CanActable = false;

			await ExexuteInternalAsync(timeAwaiter);
		}

		public void Reset()
		{
			CanActable = false;
		}


		protected virtual UniTask ExexuteInternalAsync(Ling.Utility.Async.WorkTimeAwaiter timeAwaiter) =>
			default(UniTask);

		#endregion


		#region private 関数


		#endregion
	}
}
