// 
// CharaAutoHeal.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.28
// 

using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Ling.Chara
{
	/// <summary>
	/// 一定間隔で自動で回復させる処理
	/// </summary>
	[RequireComponent(typeof(ICharaController))]
	public class CharaAutoHeal : MonoBehaviour, ICharaPostProcesser
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


		/// <summary>
		/// 優先度(値が低いもの順に実行される)
		/// </summary>
		int ICharaPostProcesser.Order { get; } = 0;

		/// <summary>
		/// 処理する必要があるか
		/// </summary>
		bool ICharaPostProcesser.ShouldExecute 
		{
			get 
			{
				if (_status.IsDead.Value) return false;

				Count();

				return _current >= _actionNum;
			}
		}

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 行動回数を増やす
		/// </summary>
		public void Count()
		{
			++_current;
		}

		/// <summary>
		/// 回復処理
		/// </summary>
		public void ExecuteHeal()
		{
			// 死んでたら何もしない
			if (_status.IsDead.Value) return;

			_status.HP.AddCurrent(_healValue);
			_current = 0;

			Utility.Log.Print($"自動回復！ +{_healValue}");
		}


		/// <summary>
		/// 非同期処理を行う
		/// </summary>
		UniTask ICharaPostProcesser.ExecuteAsync()
		{
			ExecuteHeal();

			return UniTask.FromResult(default(Unit));
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

			_charaController.Model.AddPostProcess(this);
		}

		#endregion
	}
}