// 
// CanvasGroup.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.16
// 

using UnityEngine;
using Zenject;

namespace Ling.Utility.UI
{
	/// <summary>
	/// 自動でカメラを切り替えるスクリプト
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class CanvasCategory : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private CanvasCameraType _cameraType = CanvasCameraType.UI;
		[SerializeField] private bool _enableWorldCameraChange = true;	// カメラを自動で変更するか
		
		[Inject] private CanvasCategoryManager _canvasCategoryManager = default;

		#endregion


		#region プロパティ

		public CanvasCameraType CameraType => _cameraType;
		public bool EnableWorldCameraChange => _enableWorldCameraChange;

		public Canvas Target { get; private set; }

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			Target = GetComponent<Canvas>();

			_canvasCategoryManager.Apply(this);
		}

		#endregion
	}
}