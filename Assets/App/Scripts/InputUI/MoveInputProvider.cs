// 
// MoveInputView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.01.11
// 

using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Operators;

namespace Ling.InputUI
{
	/// <summary>
	/// 移動入力を処理するView
	/// </summary>
	public class MoveInputProvider : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Button _leftButton = default;
		[SerializeField] private Button _leftUpButton = default;
		[SerializeField] private Button _upButton = default;
		[SerializeField] private Button _rightUpButton = default;
		[SerializeField] private Button _rightButton = default;
		[SerializeField] private Button _leftDownButton = default;
		[SerializeField] private Button _downButton = default;
		[SerializeField] private Button _rightDownButton = default;

		[SerializeField] private Button _dirCSwitchButton = default;

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
			_leftButton.onClick.AsObservable().Subscribe(_ => 
				{
					Debug.Log("Click");
				});
		}


		#endregion
	}
}