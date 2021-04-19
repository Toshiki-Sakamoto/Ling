//
// TimeAwaiter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.29
//

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility.Async
{
	/// <summary>
	/// 一定時間awaitを呼び出し、次に処理を移す
	/// </summary>
	public class TimeAwaiter : BaseAwaiter
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private int _waitMilliseconds;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数
		public void Setup(int milliseconds)
		{
			_waitMilliseconds = milliseconds;
		}
		public void Setup(float secods)
		{
			Setup((int)(secods * 1000));
		}

		/// <summary>
		/// 処理実行時指定時間が過ぎていた場合awaitを行う
		/// </summary>
		public override async UniTask Wait()
		{
			await UniTask.Delay(_waitMilliseconds);
		}

		public override void Reset()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
