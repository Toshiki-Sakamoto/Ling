// 
// EventSystemRemover.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.03.22
// 

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;

namespace Ling.Utility
{
	/// <summary>
	/// 起動時にEventSystemを一つにする。このスクリプトがついているシーン以外を破棄する
	/// 新しく読み込まれたシーンのなかにEventSystemがあれば無効化する
	/// </summary>
	public class EventSystemRemover : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private Scene _ownerScene = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private void RemoveEventSystemByScene(Scene scene)
		{
			// 自分のシーンは処理しない
			if (_ownerScene == scene) return;

			var eventSystemObjects = scene.GetRootGameObjects().Where(gameObject => gameObject.GetComponent<EventSystem>());
			foreach (var eventSystem in eventSystemObjects)
			{
				eventSystem.SetActive(false);
			}
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_ownerScene = gameObject.scene;

			// 現在のシーンすべてを走査する
			for (int i = 0; i < SceneManager.sceneCount; ++i)
			{
				var scene = SceneManager.GetSceneAt(i);
				RemoveEventSystemByScene(scene);
			}

			// シーン読み込み時のトリガー
			SceneManager.sceneLoaded += (scene_, loadMode_) => 
				{
					RemoveEventSystemByScene(scene_);
				};
		}

		#endregion
	}
}