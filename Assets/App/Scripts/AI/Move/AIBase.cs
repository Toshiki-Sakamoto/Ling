//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.26
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Ling.AI.Move
{
	using CharaMaster = Ling.MasterData.Chara;

	/// <summary>
	/// 移動AIのベースクラス
	/// </summary>
	public abstract class AIBase : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.Manager _mapManager = default;
		[Inject] private Chara.CharaManager _charaManager = default;

		private CharaMaster.MoveAIData _masterAIData;
		private Vector2Int? _destination;	// 目的地
		private Chara.ICharaController _unit;

		#endregion


		#region プロパティ

		/// <summary>
		/// AIの種類
		/// </summary>
		public abstract Const.MoveAIType AIType { get; }

		/// <summary>
		/// 汎用パラメータ
		/// </summary>
		public int Param1 { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(CharaMaster.MoveAIData moveAIData)
		{
			_masterAIData = moveAIData;
		}

		/// <summary>
		/// 保存してあるルートをリセットする
		/// </summary>
		public void ResetDestination()
		{
			_destination = null;
		}

		public virtual async UniTask ExecuteAsync(Chara.ICharaController unit)
		{
			_unit = unit;

			// もっとも優先すべきものがあればそこに向かって歩く
			var mustTargetPos = GetMustTarget();
			if (mustTargetPos != null)
			{

			}
			else
			{
				// 目的地があればそこに向かって歩く
				if (_destination != null)
				{

				}
			}

			// 目的地がなければ
		}

		#endregion


		#region private 関数

		protected virtual void Route()
		{

		}

		/// <summary>
		/// 優先すべきターゲット座標取得する
		/// </summary>
		protected virtual Vector2Int? GetMustTarget()
		{
			// 1番目に優先するもの
			if (_masterAIData.FirstTarget != Const.TileFlag.None)
			{

			}

			// 2番目に優先するもの
			if (_masterAIData.SecondTarget != Const.TileFlag.None)
			{
			}

			return null;
		}

		protected virtual Vector2Int? GetTargetPos(Const.TileFlag targetType)
		{
			// 同じ部屋にターゲットがいるかどうか
			return null;
		}

		#endregion
	}
}
