// 
// CharaAutoHeal.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.28
// 

using UnityEngine;

namespace Ling.Chara
{
	/// <summary>
	/// 一定間隔で自動で回復させる処理
	/// </summary>
	[RequireComponent(typeof(ICharaController))]
	public class CharaAutoHeal : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private int _actionNum = 0;	// 自動回復までの回数
		[SerializeField] private int _healValue = 0;	// 自動回復量

		private ICharaController _charaController;
		private CharaStatus _status;
		private int _current;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 行動回数を増やす
		/// </summary>
		public void Count()
		{
			if (++_current >= _actionNum)
			{
				ExecuteHeal();

				_current = 0;
			}
		}

		/// <summary>
		/// 回復処理
		/// </summary>
		public void ExecuteHeal()
		{
			// 死んでたら何もしない
			if (_status.IsDead.Value) return;

			_status.HP.AddCurrent(_healValue);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_charaController = GetComponent<ICharaController>();
			_status = _charaController.Status;
		}

		#endregion
	}
}