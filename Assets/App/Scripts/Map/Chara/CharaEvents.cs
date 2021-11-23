//
// CharaEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.09
//

using UnityEngine;

namespace Ling.Chara
{
	/// <summary>
	/// 移動した
	/// </summary>
	public class EventPosUpdate
	{
		public Vector2Int? prevPos; // 以前の座標
		public Vector2Int newPos;   // 今回の座標
		public int mapLevel;
		public CharaType charaType;
	}

	/// <summary>
	/// 削除された
	/// </summary>
	public class EventRemove
	{
		public ICharaController chara;
	}

	/// <summary>
	/// キャラが死亡した時
	/// </summary>
	public class EventDead
	{
		public ICharaController chara;
	}

	/// <summary>
	/// 倒した時
	/// </summary>
	public class EventKilled
	{
		public ICharaController unit;		// 倒した人
		public ICharaController opponent;	// 倒された相手
	}

	/// <summary>
	/// ダメージを受けた時
	/// </summary>
	public class EventDamage
	{
		public ICharaController chara;
		public long value;	// ダメージ値
	}

	/// <summary>
	/// 経験値獲得した時
	/// </summary>
	public class EventAddedExp
	{
		public ICharaController Chara;
		public int Exp;
	}

	/// <summary>
	/// レベルアップした時
	/// </summary>
	public class EventLevelUp
	{
		public ICharaController Chara;
		public int Lv;
	}

	/// <summary>
	/// HP回復
	/// </summary>
	public class EventHealHP
	{
		public ICharaController Chara;
		public long Value;
	}

	/// <summary>
	/// アイテムを手に入れた
	/// </summary>
	public class EventItemGet
	{
		public MasterData.Item.ItemMaster ItemMaster;	// 取得したアイテム
		public Chara.ICharaController Chara;
	}

	/// <summary>
    /// 敵がレベルアップしたとき
    /// </summary>
	public class EnemyLevelUp
	{
		public EnemyControl Enemy;
	}
}
