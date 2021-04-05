// 
// MoveInputScene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.03.22
// 

using UnityEngine;
using Zenject;

namespace Ling.Scenes.MoveInput
{
	/// <summary>
	/// 移動入力UI
	/// </summary>
	public class MoveInputScene : Common.Scene.Base
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		lizeField] private Common.Input.MoveInputProvider _provider = default;

		t] private Common.Input.IInputManager _inputManager = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		e void Awake()
		{
			ager?.Bind(_provider);
		}

		#endregion

	}