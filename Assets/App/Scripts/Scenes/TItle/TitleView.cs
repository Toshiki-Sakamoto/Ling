// 
// View.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.18
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;

namespace Ling.Scenes.TItle
{
	/// <summary>
	/// 
	/// </summary>
	public class TitleView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Button _buttonStart = null;

		#endregion


		#region プロパティ

		public System.Action OnClickGotoBattle { get; set; }

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
			_buttonStart.OnClickAsObservable().Subscribe(_ => OnClickGotoBattle?.Invoke());
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