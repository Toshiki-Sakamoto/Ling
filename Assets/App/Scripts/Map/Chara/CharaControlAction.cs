//
// CharaControlAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using Cysharp.Threading.Tasks;
using Utility.Extensions;
using Unity.VisualScripting;

namespace Ling.Chara
{
	/// <summary>
	/// 行動に関すること
	/// </summary>
	public interface ICharaActionController
	{
		bool ExistsAttackProcess { get; }

		/// <summary>
		/// 攻撃プロセスの実行
		/// </summary>
		void ExecuteAttackProcess();

		UniTask WaitAttackProcess();
	}

	/// <summary>
	/// 
	/// </summary>
	public partial class CharaControl<TModel, TView>
		where TModel : CharaModel
		where TView : ViewBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public bool ExistsAttackProcess => !_attackProcess.IsNullOrEmpty();
		
		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		#endregion


		#region private 関数

		#endregion
	}
}
