// 
// AssetBundleManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.15
// 

using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;

namespace Ling.Utility.AssetBundle
{
	/// <summary>
	/// AddresableAssetBundleを使用した管理
	/// </summary>
	public class AssetBundleManager : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private CancellationToken _destroyToken;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// LoadAssetAsyncをラップしたもの
		/// </summary>
		public async UniTask<TObject> LoadAssetAsync<TObject>(string address, Component owner = null) =>
			await LoadAssetAsync<TObject>(address, _destroyToken, owner);
		public async UniTask<TObject> LoadAssetAsync<TObject>(string address, CancellationToken token, Component owner = null)
		{
			var result = await Addressables.LoadAssetAsync<TObject>(address)
				.WithCancellation(token);

			// ownerがいれば、削除時に開放させるように自動的に結びつける
			owner?.OnDestroyAsObservable()
				.Subscribe(_ => 
				{
					Addressables.Release(result);
				});

			return result;
		}

		/// <summary>
		/// InstantiateAsyncをラップしたもの
		/// </summary>
		public async UniTask<TObject> InstantiateAsync<TObject>(string address, Transform parent, bool instantiateInWorldSpace, CancellationToken token)
		{
			var result = await Addressables.InstantiateAsync(address, parent, instantiateInWorldSpace, trackHandle: true)
				.WithCancellation(token);

			// instantiateの場合は所有者が消える時に自動でReleaseしてくれる

			return result.GetComponent<TObject>();
		}

		public void Release<TObject>(TObject obj)
		{
			Addressables.Release<TObject>(obj);
		}


		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_destroyToken = this.GetCancellationTokenOnDestroy();
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}