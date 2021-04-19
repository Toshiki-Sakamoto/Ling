// 
// SortingLayerChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.07.15
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Utility.Renderer
{
	/// <summary>
	/// コンポーネント下のSortingLayerを一度に切り替える
	/// </summary>
	[ExecuteInEditMode]
	public class SortingLayerChanger : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField, Attribute.SortingLayer]
		private string _layerName = "Default";

		[SerializeField]
		private int _orderInLayer = default;

		[SerializeField]
		private bool _isIncludeChildren = default;

		#endregion


		#region プロパティ

		public string LayerName
		{
			get => _layerName;
			set
			{
				_layerName = value;
				foreach (var renderer in GetComponentsInChildren<UnityEngine.Renderer>(includeInactive: true))
				{
					renderer.sortingLayerName = _layerName;
				}
			}
		}

		public int OrderInLayer
		{
			get => _orderInLayer;
			set
			{
				_orderInLayer = value;
				foreach (var renderer in GetComponentsInChildren<UnityEngine.Renderer>(includeInactive: true))
				{
					renderer.sortingOrder = _orderInLayer;
				}
			}
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			LayerName = _layerName;
			OrderInLayer = _orderInLayer;
		}

		private void OnValidate()
		{
			LayerName = _layerName;
			OrderInLayer = _orderInLayer;
		}

		#endregion
	}
}