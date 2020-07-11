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

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public async UniTask SetupAsync()
		{
			await SetupAsyncInternal();
		}

		/// <summary>
		/// キャラ情報からCharaModelを作成する
		/// </summary>
		/// <returns></returns>
		public CharaModel CreateModel()
		{
			var model = new CharaModel();


			return model;
		}

		public void Refresh()
		{

		}

		#endregion


		#region private 関数

		protected virtual UniTask SetupAsyncInternal() =>
			UniTask.FromResult(default(object));

		#endregion
	}
}
