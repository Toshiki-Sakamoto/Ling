// 
// BattleDebugView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.29
// 

using UnityEngine;
using UnityEngine.UI;

namespace Ling.Scenes.Battle._Debug
{
	/// <summary>
	/// Battle Debug View
	/// </summary>
	public class BattleDebugView : MonoBehaviour 
    {
		#region 定数, class, enum

		[SerializeField]
		public class DebugTopPanel
		{
			[SerializeField] private Toggle _tileScoreShowToggle = default;
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
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