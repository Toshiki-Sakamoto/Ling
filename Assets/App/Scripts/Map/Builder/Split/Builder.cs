//
// Builder.cs
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
using Zenject;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 
	/// </summary>
	public class Builder : BuilderBase 
	{
		#region 定数, class, enum

		public class Factory : PlaceholderFactory<Builder>
		{ 
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private SplitBuilderFactory _splitFactory = null;     // 部屋の分割担当

		private ISplitter _splitter = null;

		#endregion


		#region プロパティ

		public MapRect MapRect { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

#if false
		public Builder()
			: this(new TSplitter())
		{
		}

		public Builder(ISplitter splitter)
		{
			_splitter = splitter;

			_mapRect = new MapRect();
		}
#endif

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 処理を実行する
		/// </summary>
		protected override IEnumerator<float> ExecuteInternal()
		{
			_splitter = _splitFactory.Create();

			MapRect = new MapRect();

			// 全体を一つの区画にする
			MapRect.CreateRect(0, 0, Width - 1, Height - 1);

			// 区画を作る
			var enumerator = _splitter?.SplitRect(_data, MapRect);
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}
		}


		#endregion


		#region private 関数

		/// <summary>
		/// 部屋を作成する
		/// </summary>
		/// <returns></returns>
        private IEnumerator CreateRoom()
        {
            for (int i = 0; i < MapRect.RectCount; ++i)
            {
				var rectData = MapRect[i];
				var rect = rectData.rect;

				// 矩形の大きさを計算
				var w = rect.width - 3;
				var h = rect.height - 3;

				// 区画に入る最小部屋の余裕を求める
				var cw = w - _data.RoomMinSize;
				var ch = h - _data.RoomMinSize;

				// 部屋の大きさを決定する
				var sw = UnityEngine.Random.Range(0, cw + 1);
				var sh = UnityEngine.Random.Range(0, ch + 1);
				var rw = w - sw;
				var rh = h - sh;

				// 部屋の位置を決定する
				var rx = UnityEngine.Random.Range(0, sw + 1) + 2;
				var ry = UnityEngine.Random.Range(0, sh + 1) + 2;

				// 求めた結果から部屋の情報を設定
				rectData.room.xMin = rect.xMin + rx;
				rectData.room.yMin = rect.yMin + ry;
				rectData.room.xMax = rect.xMin + rw;
				rectData.room.yMax = rect.yMin + rh;

				var room = rectData.room;

                // 部屋を作る
				TileDataMap.FillRect(room.xMin, room.yMin, room.xMax, room.yMax, Const.TileFlag.Floor);
			}

			yield return null;
        }

		#endregion
	}
}
