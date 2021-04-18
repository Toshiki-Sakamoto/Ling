//
// CharaLoader.cs
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
	/// 1キャラクタを生成するための読み込み処理＋生成処理を兼ね備えたもの
	/// </summary>
	public class CharaLoader : MonoBehaviour
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

		/// <summary>
		/// 指定Prefabのインスタンスを非同期で作成、読み込みする
		/// </summary>
		public static async UniTask<T> LoadAsync<T>(T prefab, Transform parent, GameObject owner = null) where T : UnityEngine.Object
		{
			var instance = owner.AddComponent<CharaLoader>();

			var result = await instance.LoadAsyncInternal(prefab, parent);

			GameObject.Destroy(instance);

			return result;
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 実際読み込み処理
		/// </summary>
		private async UniTask<T> LoadAsyncInternal<T>(T prefab, Transform parent) where T : UnityEngine.Object
		{
			// 今後何かあったときここを非同期で待機できる
			var instance = GameObject.Instantiate<T>(prefab, parent);
			if (instance == null)
			{
				return null;
			}

			// 必要なコンポーネントをアタッチする
			var instanceGameObject = instance as GameObject;


			return instance;
		}

		#endregion
	}
}
