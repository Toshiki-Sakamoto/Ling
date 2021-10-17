//
// ICharaController.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.10.10
//

using UnityEngine;
using System.Linq;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Ling.Map.TileDataMapExtensions;
using Ling.Const;
using UnityEngine.Tilemaps;
using Utility.Extensions;
using System;
using Utility;

namespace Ling.Chara
{
	/// <summary>
	/// 簡易Controller参照用インターフェース
	/// </summary>
	public interface ICharaController : Map.IMapObject
	{
		CharaModel Model { get; }

		ViewBase View { get; }

		CharaStatus Status { get; }

		ICharaMoveController MoveController { get; }
		Exp.ICharaExpController ExpController { get; }

		CharaEquipControl EquipControl { get; }

		
		/// <summary>
		/// キャラ名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Tilemap情報を設定する
		/// </summary>
		void SetTilemap(Tilemap tilemap, int mapLevel);

		/// <summary>
		/// ダメージを受けた時
		/// </summary>
		UniTask Damage(long value);

		TProcess AddMoveProcess<TProcess>() where TProcess : Utility.ProcessBase;
		TProcess AddAttackProcess<TProcess>() where TProcess : Utility.ProcessBase;
	}
}
