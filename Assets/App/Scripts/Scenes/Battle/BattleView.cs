// 
// View.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.13
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class BattleView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager = default;

		[SerializeField] private Message.MessageView _messageView = null;
		[SerializeField] private UI.UIHeaderView _uiHeaderView = null;

		#endregion


		#region プロパティ

		public Message.MessageView MessageView => _messageView;
		public UI.UIHeaderView UIHeaderView => _uiHeaderView;

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
			_messageView.Setup();
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