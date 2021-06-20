//
// CommonDebugMenu.cs
// ProductName Ling
//
// Created by  on 2021.06.11
//
using UnityEditor;
	
namespace Ling.Common._Debug.Editor
{
	public static class CommonDebugMenu
	{
		private const string ForceResumeKey = "Ling/Common/Force Resume";
		
		public static bool IsForceResumeChecked => Menu.GetChecked(ForceResumeKey);
		
		/// <summary>
		/// 強制的にResumeとするか
		/// </summary>
		[MenuItem(ForceResumeKey)]
		private static void OnForceResume()
		{
			Menu.SetChecked(ForceResumeKey, !IsForceResumeChecked);
		}
	}
}
