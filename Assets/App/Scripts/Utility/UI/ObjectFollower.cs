// 
// 3DUIController.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.28
// 

using UnityEngine;

namespace Ling.Utility.UI
{
	/// <summary>
	/// 指定したターゲットオブジェクトを追従する
	/// </summary>
	public class ObjectFollower : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _target = default;
		[SerializeField] private Camera _camera = default;

		private RectTransform _rectTransform;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetTarget(Transform target) =>
			_target = target;

		public void SetCamera(Camera camera) =>
			_camera = camera;

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		private void LateUpdate()
		{
			if (_target == null) return;

			if (_camera == null)
			{
				_camera = Camera.main;
			}

			_rectTransform.position = RectTransformUtility.WorldToScreenPoint(_camera, _target.position);	
		}

		#endregion
	}
}