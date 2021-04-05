// 
// PlayerControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.11
// 

using UnityEngine;
using System;
using Cinemachine;

namespace Ling.Chara
{
	/// <summary>
	/// Player Control
	/// </summary>
	public class PlayerControl : CharaControl<PlayerModel, PlayerView>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		/// <summary>
		/// 削除時に呼び出される
		/// </summary>
		public IObserver<PlayerControl> OnDestroyed;

		#endregion


		#region private 変数

		[SerializeField] private Utility.CameraFollow _cameraFollow = default;
		[SerializeField] private CinemachineVirtualCamera _cinemaVCamera = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetFollowCameraEnable(bool enable) =>
			_cinemaVCamera.enabled = enable;
		//_cameraFollow.enabled = enable;

		protected override void DestroyProcessInternal()
		{
			OnDestroyed?.OnNext(this);
			OnDestroyed?.OnCompleted();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}