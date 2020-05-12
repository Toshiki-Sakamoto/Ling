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

		private Map.Builder.IBuilder _builder;
		private IEnumerator<float> _buildeEnumerator = null;

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
			_view.Setup();

			_view.OnExecute = (setting_) =>
				{
					var builderData = new Map.Builder.BuilderData();

					_builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
					_builder.Initialize(setting_.Width, setting_.Height);

					_builder.SetData(builderData);

					_buildeEnumerator = _builder.ExecuteDebug(null);

					// ランダム値を変化させたい場合
					if (setting_.RandomSeed > 0)
					{
                        Random.InitState(setting_.RandomSeed);
					}

					_view.MapDrawView.Setup(setting_.Width, setting_.Height, Map.Builder.BuilderConst.BuilderType.Split);
					_view.MapDrawView.DrawUpdate(_builder);
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
			if (_builder == null || _buildeEnumerator == null) return;
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