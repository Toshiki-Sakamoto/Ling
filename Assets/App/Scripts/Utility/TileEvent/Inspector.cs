// 
// Inspector.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.10.17.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Ling.Utility.TileEvent
{
	/// <summary>
	/// 
	/// </summary>
	[CustomEditor(typeof(TileEvents))]
	public class Inspector : UnityEditor.Editor
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

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		public override void OnInspectorGUI()
		{
			var map = (TileEvents)target;

			map.SetDeleted(true);
			map.CreateTilesList(true);
			map.RemoveDeleteTiles(true);

			DrawDefaultInspector();
		}

		#endregion
	}
}
#endif