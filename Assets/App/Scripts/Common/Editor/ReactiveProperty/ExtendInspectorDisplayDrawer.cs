//
// ExtendInspectorDisplayDrawer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.12
//

using UniRx;
using Ling.Common.ReactiveProperty;

namespace Ling.Common.Editor.ReactiveProperty
{
	/// <summary>
	/// インスペクタの表示の向上とイベントの通知が可能になる
	/// カスタムReactiveProperyを作成したら
	/// [UnityEditor.CustomPropertyDrawer(typeof(XXXX))]
	/// を追加していくこと。
	/// クラス自体は一つあればいい
	/// </summary>
	[UnityEditor.CustomPropertyDrawer(typeof(Vector2IntReactiveProperty))]
	[UnityEditor.CustomPropertyDrawer(typeof(Vector3IntReactiveProperty))]
	public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
    {
	}
}
