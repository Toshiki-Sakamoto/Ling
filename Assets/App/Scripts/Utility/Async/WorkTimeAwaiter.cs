//
// TimeAwaiter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.22
//

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ling.Utility.Async
{
	/// <summary>
	/// 指定時間が過ぎた場合、awaitを呼び出す。
	/// 外部から経過時間を設定するようにしてもいいかも
	/// </summary>
	public class WorkTimeAwaiter : BaseAwaiter
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private float _waitTimeSecond;
		private float _timeCount;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(float second)
		{
			_waitTimeSecond = second;

			Reset();
		}

		/// <summary>
		/// 処理実行時指定時間が過ぎていた場合awaitを行う
		/// </summary>
		public override async UniTask Wait()
		{
			var diff = Time.realtimeSinceStartup - _timeCount;
			if (diff >= _waitTimeSecond)
			{
				Reset();

				await UniTask.DelayFrame(1);
			}
		}

		public override void Reset()
		{
			_timeCount = Time.realtimeSinceStartup;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
