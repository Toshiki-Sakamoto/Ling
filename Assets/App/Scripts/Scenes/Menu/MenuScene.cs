// 
// MenuScene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.07
// 

using UnityEngine;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// Menu Scene
	/// </summary>
	public class MenuScene : Common.Scene.Base
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private MenuModel _model = default;
		[SerializeField] private MenuView _view = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene() 
		{
			var menuArgument = Argument as MenuArgument;

			_model.SetArgument(menuArgument);
		}

		/// <summary>
		/// StartScene後呼び出される
		/// </summary>
		public override void UpdateScene() 
		{ }

		/// <summary>
		/// シーン終了時
		/// </summary>
		public override void StopScene() 
		{ }

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