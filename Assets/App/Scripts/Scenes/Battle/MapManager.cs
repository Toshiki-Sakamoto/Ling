// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class MapManager : Utility.MonoSingleton<MapManager> 
    {
		#region 定数, class, enum

		public class Cache
		{
			public int CacheMax { get; set; }

			private Transform _cacheRoot;

			public void Setup(Transform cacheRoot)
			{
				_cacheRoot = cacheRoot;
			}
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _root = null;
		[SerializeField] private Transform _cacheRoot = null;   // 非アクティブタイルをキャッシュしておくところ

		[Inject] private Map.Builder.IManager _builderManager = null;

		private Cache _cache = new Cache();

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 予め決められた数のタイルオブジェクトを作成し、キャッシュしておく 
		/// </summary>
		public void Initialize(int maxWidth, int maxHeight)
		{

		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

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
		void OnDestoroy()
		{
		}

		#endregion
	}
}