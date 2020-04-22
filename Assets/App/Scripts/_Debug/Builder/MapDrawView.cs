// 
// MapDrawView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.20
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling._Debug.Builder
{
	/// <summary>
	/// 
	/// </summary>
	public class MapDrawView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Sprite _mapRectSprite = null;	// 区画
		[SerializeField] private Sprite _floorSprite = null;	// 床`

		private int _width, _height;
		private GameObject[] _drawObjects;
		private Map.Builder.Const.BuilderType _builderType;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup(int width, int height, Map.Builder.Const.BuilderType builderType)
		{
			_width = width;
			_height = height;
			_builderType = builderType;
			_drawObjects = new GameObject[width * height];

			switch (_builderType)
			{
				case Map.Builder.Const.BuilderType.Split:
					SetUp_Split();
					break;

				default:
					break;
			}
		}

		public void DrawUpdate(Map.Builder.BuilderBase builder)
		{
			switch (_builderType)
			{
				case Map.Builder.Const.BuilderType.Split:
					DrawUpdate_Split(builder);
					break;

				default:
					break;
			}
		}

		#endregion


		#region private 関数

		private void SetUp_Split()
		{

		}

		private void DrawUpdate_Split(Map.Builder.BuilderBase builder)
		{
			var splitBuilder = builder as Map.Builder.Split.Builder;
			if (splitBuilder == null) return;

			// 区画情報
			var mapRect = splitBuilder.MapRect;
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}