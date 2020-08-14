//
// CharaFinder.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.13
//

using UnityEngine;
using Zenject;

namespace Ling.AI.AIUtility
{
	/// <summary>
	/// キャラクタ検索便利関数群
	/// </summary>
	public class CharaFinder : MonoBehaviour
    {
		
	#if false
		#region 定数, class, enum

		public class Param 
		{
			public bool needsDiagonal = true;	// 斜めも必要か
			public bool isEnemyIgnore = false;	// 敵を無視するか
			public bool needsSameRoom = true;	// 同じ部屋であることが必要か(部屋じゃない場合1マスしか見ない)
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager = default;
		[Inject] private Scenes.Battle.MapManager _mapManager = default;	

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// Playerが隣接しているか
		/// </summary>
		/// <param name="charaModel">検索元</param>
		/// <returns>隣接している場合true</returns>
		public bool IsPlayerAdjacent(Chara.CharaModel charaModel, Param param) =>
			IsCharaAdjacent(charaModel.MapLevel, charaModel.Pos, Chara.CharaType.Player, param);

		/// <summary>
		/// 指定キャラクタタイプと隣接しているか
		/// </summary>
		/// <param name="srcPos">検索地点</param>
		/// <param name="targetType">検索対象</param>
		/// <returns>隣接している場合true</returns>
		public bool IsCharaAdjacent(int level, in Vector2Int srcPos, Chara.CharaType targetType, Param param) =>
			ExistsCharaWithInCell(level, srcPos, targetType, 1, param);

		/// <summary>
		/// 指定マス以内にPlayerが存在するか
		/// </summary>
		/// <param name="charaModel">検索元</param>
		/// <param name="cellNum">マス数</param>
		/// <param name="needsDiagonal">斜めも必要かどうか</param>
		/// <returns>存在する場合true</returns>		
		public bool ExistsPlayerWithInCell(Chara.CharaModel charaModel, int cellNum, Param param) =>
			ExistsCharaWithInCell(charaModel.MapLevel, charaModel.Pos, Chara.CharaType.Player, cellNum, param);

		/// <summary>
		/// 指定マス以内にキャラクタが存在するか
		/// </summary>
		/// <param name="srcPos">検索地点</param>
		/// <param name="targetType">検索対象の種類</param>
		/// <param name="cellNum">マス数</param>
		/// <param name="Param">検索パラメータ</param>
		/// <returns>存在する場合true</returns>
		public bool ExistsCharaWithInCell(int level, in Vector2Int srcPos, Chara.CharaType targetType, int cellNum, Param param) =>
			TryGetCharaPosWithInCell(level, srcPos, targetType, cellNum, param, out var targetPos);

		/// <summary>
		/// 指定したキャラクタが、指定マス以内に存在する場合trueを返し、座標を取得する。
		/// </summary>
		/// <param name="srcPos">検索地点</param>
		/// <param name="targetType">検索対象の種類</param>
		/// <param name="cellNum">マス数</param>
		/// <param name="param">検索パラメータ</param>
		/// <param name="targetPos">存在する場合ターゲットの座標を返す</param>
		/// <returns>存在する場合true</returns>
		public bool TryGetCharaPosWithInCell(int level, in Vector2Int srcPos, Chara.CharaType targetType, int cellNum, Param param, out Vector2Int targetPos)
		{
			targetPos = Vector2Int.zero;

			// 指定階層のマップを取得
			var tileDataMap = _mapManager.MapControl.FindTileDataMap(level);
			if (tileDataMap == null) return false;


			return true;
		}

		public 

		#endregion

		#region private 関数

		#endregion
	#endif
	}
}
