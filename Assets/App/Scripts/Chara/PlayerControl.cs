// 
// PlayerControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.11
// 

using UnityEngine;

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

		#endregion


		#region private 変数

		[SerializeField] private Utility.CameraFollow _cameraFollow = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetFollowCameraEnable(bool enable) =>
			_cameraFollow.enabled = enable;

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}