//
// FrameAwaiter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.25
//

using Cysharp.Threading.Tasks;

namespace Ling.Utility.Async
{
	/// <summary>
	/// 指定フレームが経過するまでawaitを呼び出す
	/// </summary>
	public class FrameAwaiter : BaseAwaiter
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private float _waitFrame;
		private int _frameCount;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(int waitFrame)
		{
			_waitFrame = waitFrame;

			Reset();
		}

		/// <summary>
		/// 処理実行時指定時間が過ぎていた場合awaitを行う
		/// </summary>
		public override async UniTask Wait()
		{
			if (_frameCount++ < _waitFrame)
			{
				await UniTask.DelayFrame(1);
			}

			Reset();
		}

		public override void Reset()
		{
			_frameCount = 0;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
