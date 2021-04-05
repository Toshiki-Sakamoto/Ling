// 
// TouchPointEventTriggerEditor.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.28
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.EventSystems;
using Ling.Utility;



namespace Ling.Editor.Utility
{
	/// <summary>
	/// 
	/// </summary>
	[CustomEditor(typeof(TouchPointEventTrigger))]
	public class TouchPointEventTriggerEditor : EventTriggerEditor
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			base.OnInspectorGUI();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}