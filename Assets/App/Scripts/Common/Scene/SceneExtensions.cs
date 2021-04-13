//
// SceneExtensions.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.13
//

using System.Linq;

namespace Ling.Common.Scene.Extensions
{
	/// <summary>
	/// Base.cs の拡張
	/// </summary>
	public static class SceneExtensions
	{
		public static Base FindAddSceneBySceneData(this Base self, SceneData sceneData) =>
			self.Children.Find(scene => scene.SceneData == sceneData);

		/// <summary>
		/// 子供として自分のデータに追加する
		/// </summary>
		public static void AddChild(this Base self, Base child)
		{
			self.Children.Add(child);

			// データとしても追加する
			self.SceneData.AddChild(child.SceneData);
		}

		/// <summary>
		/// 子供のデータを削除する
		/// </summary>
		public static void RemoveChild(this Base self, Base child)
		{
			self.Children.Remove(child);

			// データとしても削除する
			self.SceneData.RemoveChild(child.SceneData);
		}
	}
}
