//
// Launcher.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.01.07
//

namespace Ling
{
	/// <summary>
	/// プロジェクト側のLauncher
	/// Common.Launcherを継承して使用する
	/// </summary>
	public class Launcher : Common.Launcher
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

		#endregion


		#region private 関数

		protected override void Awake()
		{	
#if DEBUG		
			_debugManager.Setup(new _Debug.DebugRootMenuData());
#endif

			base.Awake();
		}

		#endregion
	}
}
