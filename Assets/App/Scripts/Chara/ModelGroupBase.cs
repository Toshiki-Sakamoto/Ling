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
	public abstract class ModelGroupBase : IEnumerable<CharaModel>
    {
		#region 定数, class, enum

		public struct Enumerator : IEnumerator<CharaModel>
		{
			private readonly List<CharaModel> _list;
			private int _index;

			public Enumerator(List<CharaModel> list)
			{
				_list = list;
				_index = -1;
			}

			public CharaModel Current => _list[_index];
			object IEnumerator.Current => _list[_index];

			public void Dispose() {}
			public bool MoveNext() => ++_index < _list.Count();
			public void Reset() => _index = 0;

		}

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

		public IEnumerator<CharaModel> GetEnumerator() => new Enumerator(Models);

		IEnumerator IEnumerable.GetEnumerator() => new Enumerator(Models);

		public void SetTilemap(Tilemap tilemap)
		{
			_tilemap = tilemap;
		}

		public async UniTask SetupAsync()
		{
			await SetupAsyncInternal();
		}

		/// <summary>
		/// 削除される直前に呼び出し
		/// </summary>
		public virtual void OnDestroy() { }

		#endregion


		#region private 関数

		protected virtual UniTask SetupAsyncInternal() =>
			UniTask.FromResult(default(object));

		#endregion
	}
}
