//
// VectorIntReactiveProperty.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.12
//

using UniRx;
using UnityEngine;

namespace Ling.Common.ReactiveProperty
{
	/// <summary>
	/// VectorIntXX ReactiveProperty
	/// 追加した場合 Editor.ExtendInspectorDisplayDrawerにも追加していくこと
	/// </summary>

	[System.Serializable]
	public class Vector2IntReactiveProperty : ReactiveProperty<Vector2Int>
	{
		public Vector2IntReactiveProperty() {}

		public Vector2IntReactiveProperty(Vector2Int initialValue)
			:base(initialValue)
		{}
	}

	[System.Serializable]
	public class Vector3IntReactiveProperty : ReactiveProperty<Vector3Int>
	{
		public Vector3IntReactiveProperty() {}

		public Vector3IntReactiveProperty(Vector3Int initialValue)
			:base(initialValue)
		{}
	}
}
