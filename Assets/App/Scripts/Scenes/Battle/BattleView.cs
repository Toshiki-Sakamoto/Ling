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

		[SerializeField] private BattleMap.MapView _mapView = null;
		[SerializeField] private BattleMap.MiniMapView _miniMapView = null;
		[SerializeField] private Message.MessageView _messageView = null;

		#endregion


		#region プロパティ

		public BattleMap.MapView MapView => _mapView;
		public BattleMap.MiniMapView MiniMap => _miniMapView;
		public Message.MessageView MessageView => _messageView;

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