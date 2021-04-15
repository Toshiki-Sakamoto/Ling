// 
// SceneCameraManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.14
// 

using UnityEngine;
using UnityEngine.SceneManagement;


namespace Ling.Common.Scene
{
	/// <summary>
	/// 読み込まれたシーン内のCanvasのCameraを管理する
	/// UIであればUI用のCameraをアタッチ
	/// もしカメラ操作を受け付けたくない場合は"CameraManagerOutOfControl"スクリプトをCanvasにアタッチ
	/// </summary>
	[RequireComponent(typeof(ExSceneManager))]
	public class SceneCameraManager : MonoBehaviour 
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

		private void Execute(UnityEngine.SceneManagement.Scene scene)
		{

		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			// シーン読み込み時のトリガー
			SceneManager.sceneLoaded += (scene_, loadMode_) =>
				{
					Execute(scene_);
				};
		}

		#endregion
	}
}