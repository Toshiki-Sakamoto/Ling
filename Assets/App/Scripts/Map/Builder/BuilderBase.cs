//
// Base.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.22
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder
{
	public interface IBuilder
	{
		/// <summary>
		/// 実行中
		/// </summary>
		bool IsExecuting { get; }

		/// <summary>
		/// マップデータを取得する
		/// </summary>
		TileDataMap TileDataMap { get; }


		void Initialize(int width, int height);

		void SetData(BuilderData data);

		/// <summary>
		/// 処理を実行する
		/// </summary>
		UniTask Execute();

		IEnumerator<float> ExecuteDebug();

		/// <summary>
		/// プレイヤーの初期座標をランダムに取得する
		/// </summary>
		/// <returns></returns>
		Vector3Int GetPlayerInitPosition();
	}


	/// <summary>
	/// ビルダーベースクラス
	/// すべてのビルダーはこれを継承して使用する
	/// </summary>
    public abstract class BuilderBase : IBuilder
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		protected BuilderData _data = null;		// ビルダー情報

        #endregion


        #region プロパティ

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; private set; }

		/// <summary>
		/// 実行中
		/// </summary>
		public bool IsExecuting { get; private set; }

		/// <summary>
		/// マップデータを取得する
		/// </summary>
		public TileDataMap TileDataMap { get; } = new TileDataMap();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Initialize(int width, int height)
        {
			TileDataMap.Initialize(width, height);
			Width = width;
            Height = height;
        }

		/// <summary>
		/// ビルダー情報を設定する
		/// </summary>
		/// <param name="data"></param>
		public void SetData(BuilderData data) => _data = data;

        /// <summary>
        /// 処理を実行する
        /// </summary>
        public async UniTask Execute()
        {
			// 最初はすべて壁にする
			TileDataMap.AllTilesSetWall();

			IsExecuting = true;

			await ExecuteInternal();

			IsExecuting = false;
		}

		public IEnumerator<float> ExecuteDebug()
		{
			// 最初はすべて壁にする
			TileDataMap.AllTilesSetWall();

			IsExecuting = true;

			var enumerator = ExecuteInternal();
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}

			IsExecuting = false;
		}

		/// <summary>
		/// プレイヤーの初期座標をランダムに取得する
		/// </summary>
		/// <returns></returns>
		public virtual Vector3Int GetPlayerInitPosition()
		{
			// todo: 仮
			return new Vector3Int(10, 10, 0);
		}

		#endregion


		#region private 関数

		protected abstract IEnumerator<float> ExecuteInternal();

        #endregion
    }
}
