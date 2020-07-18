//
// CharaView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.12
//

using Ling.Utility.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// 
	/// </summary>
	public class CharaView : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Player _player = null;
		[SerializeField] private Chara.EnemyPoolManager _enemyPoolManager = null;

		private Dictionary<CharaModel, Enemy> _enemyViews = new Dictionary<CharaModel, Enemy>();

		#endregion


		#region プロパティ

		public Player Player => _player;

		public Chara.EnemyPoolManager EnemyPoolManager => _enemyPoolManager;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 敵のViewを一つプールから取り出す
		/// </summary>
		/// <returns></returns>
		public Enemy GetEnemyByPool(CharaModel charaModel)
		{
			var enemy = EnemyPoolManager.Pop<Enemy>(EnemyType.Normal);
			_enemyViews.Add(charaModel, enemy);

			return enemy;
		}

		/// <summary>
		/// 敵Viewを検索して取得
		/// </summary>
		public Enemy FindEnemy(CharaModel model) =>
			_enemyViews[model];

		/// <summary>
		/// 指定したModelのViewを削除(プールに戻す)
		/// </summary>
		public void RemoveChara(CharaModel charaModel)
		{
			if (!_enemyViews.TryGetValue(charaModel, out var enemy))
			{
				// 存在しない
				return;
			}

			var poolItem = enemy.GetComponent<PoolItem>();
			poolItem?.Detach();

			_enemyViews.Remove(charaModel);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
