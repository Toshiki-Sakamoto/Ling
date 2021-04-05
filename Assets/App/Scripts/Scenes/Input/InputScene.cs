// 
// InputScene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.03.22
// 

using UnityEngine;
using Zenject;

namespace Ling.Scenes.Input
{
	/// <summary>
	/// 入力UI
	/// </summary>
	public class InputScene : Common.Scene.Base
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Common.Input.MoveInputProvider _provider = default;

		[Inject] private Common.Input.IInputManager _inputManager = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			_inputManager?.Bind(_provider);
		}

		#endregion

	}
}
