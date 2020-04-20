// 
// BuilderDebug.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.20
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ling._Debug.Builder
{
	/// <summary>
	/// 
	/// </summary>
	public class BuilderDebug : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private View _view = null;

		[Inject] private Map.Builder.IManager _builderManager = null;
		[Inject] private Map.Builder.BuilderFactory _builderFactory = null;

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
			_view.OnExecute = (setting_) =>
			{
				var builderData = new Map.Builder.BuilderData();

				var builder = _builderFactory.Create(Map.Builder.Const.BuilderType.Split);
				builder.Initialize(setting_.Width, setting_.Height);

				builder.Execute();
			};
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
		void OnDestoroy()
		{
		}

		#endregion
	}
}