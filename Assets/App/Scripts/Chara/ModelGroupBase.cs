//
// ModelGroupBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// 複数のCharaModelを管理する
	/// </summary>
	public abstract class ModelGroupBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Tilemap _tilemap;

		#endregion


		#region プロパティ

		/// <summary>
		/// このグループが管理しているCharaModel
		/// </summary>
		public List<CharaModel> Models { get; } = new List<CharaModel>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetTilemap(Tilemap tilemap)
		{
			_tilemap = tilemap;
		}

		public async UniTask SetupAsync()
		{
			await SetupAsyncInternal();
		}

		public void Refresh()
		{

		}

		/// <summary>
		/// 削除される直前に呼び出し
		/// </summary>
		public void OnDestroy()
		{

		}

		#endregion


		#region private 関数

		protected virtual UniTask SetupAsyncInternal() =>
			UniTask.FromResult(default(object));

		#endregion
	}
}
