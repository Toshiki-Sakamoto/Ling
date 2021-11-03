//
// DirectionType.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.10.10
//

using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ling.Map
{
	/// <summary>
	/// 
	/// </summary>
	public enum DirectionType
	{
		Top,
		TopRight,
		Right,
		BottomRight,
		Bottom,
		BottomLeft,
		Left,
		TopLeft,
	}

	public static class DirectionTypeUtility
	{

		/// <summary>
		/// 各方角（最大８方向）指定デリゲートを呼び出す
		/// </summary>
		public static bool CallDirection(System.Func<DirectionType, bool> func)
		{
			foreach (DirectionType type in Enum.GetValues(typeof(DirectionType)))
			{
				if (func(type))
				{
					return true;
				}
			}

			return false;
		}
	}

	public static class DirectionTypeExtensions
	{
		private static Dictionary<DirectionType, Vector2Int> DirectionDict = new Dictionary<DirectionType, Vector2Int>
		{
			[DirectionType.Top] 		= new Vector2Int( 0,  1),
			[DirectionType.TopRight] 	= new Vector2Int( 1,  1),
			[DirectionType.Right] 		= new Vector2Int( 1,  0),
			[DirectionType.BottomRight] = new Vector2Int( 1, -1),
			[DirectionType.Bottom] 		= new Vector2Int( 0, -1),
			[DirectionType.BottomLeft] 	= new Vector2Int(-1, -1),
			[DirectionType.Left] 		= new Vector2Int(-1,  0),
			[DirectionType.TopLeft] 	= new Vector2Int(-1,  1),
		};

		public static Vector2Int ToVec2Int(this DirectionType self) =>
			DirectionDict[self];

		public static DirectionType ToDirectionType(this Vector2Int self)
		{
			foreach (var pair in DirectionDict)
			{
				if (self == pair.Value) return pair.Key;
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// 斜めの場合true
		/// </summary>
		public static bool IsDiagonal(this DirectionType self) =>
			self == DirectionType.TopRight ||
			self == DirectionType.TopLeft ||
			self == DirectionType.BottomRight ||
			self == DirectionType.BottomRight;
	}
}
