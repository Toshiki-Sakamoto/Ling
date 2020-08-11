//
// PlayerFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using Cysharp.Threading.Tasks;
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
	/// <see cref="Chara.ViewBase"/>の作成＆セットアップを行う
	/// </summary>
	public class CharaFactory
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private CharaModel _model;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public CharaFactory(GameObject prefab, Transform root, CharaModel model)
		{
			_model = model;
		}

		/// <summary>
		/// 指定Prefabからキャラクタを生成する
		/// </summary>
		public async UniTask<T> CreateAsync<T>(GameObject prefab, Transform root) where T : Chara.ViewBase
		{
			var instance = await CharaLoader.LoadAsync(prefab, root);
			if (instance == null)
			{
				Utility.Log.Assert(false, "生成に失敗しました");
				return default(T);
			}

			var chara = instance.GetComponent<T>();

			return chara;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
