// 
// GameManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.27
// 

using UnityEngine;
using Zenject;

namespace Ling.Common
{
	/// <summary>
	/// GameManagerとして全体の管理を行う
	/// ゲーム全体で必要無データ類を持つ
	/// 
	/// このクラス自体はシングルトンとして機能する
	/// </summary>
	public class GameManager : Utility.MonoSingleton<GameManager>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private MasterData.IMasterHolder _masterHolder;

		#endregion


		#region プロパティ

		public MasterData.IMasterHolder MasterHolder => _masterHolder;

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}