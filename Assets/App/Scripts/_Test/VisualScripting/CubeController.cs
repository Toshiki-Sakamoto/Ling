//
// CubeController.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.09.19
//

using UnityEngine;

namespace Ling._Test.VisualScripting
{
	/// <summary>
	/// 
	/// </summary>
	public class CubeController : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		void Update()
		{
			if (Input.GetKey (KeyCode.UpArrow)) 
			{
				transform.Translate(0.0f, 0.0f, 0.1f);
			}
			
			if (Input.GetKey (KeyCode.DownArrow)) 
			{
				transform.Translate(0.0f, 0.0f, -0.1f);
			}
			

			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				transform.Translate(-0.1f, 0.0f, 0.0f);
			}
			
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				transform.Translate(0.1f, 0.0f, 0.0f);
			}
		}

		#endregion
	}
}
