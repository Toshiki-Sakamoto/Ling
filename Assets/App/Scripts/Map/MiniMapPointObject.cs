// 
// MiniMapPointObject.cs  
// ProductName Ling
//  
// Created by  on 2021.09.13
// 

using UnityEngine;
using Utility;
using Zenject;
using Utility.ShaderEx;

namespace Ling.Map
{
	/// <summary>
	/// ミニマップ上のオブジェクト
	/// </summary>
	[RequireComponent(typeof(MeshRenderer))]
	public class MiniMapPointObject : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private Utility.ShaderEx.IShaderContainer _shaderContainer;

		[SerializeField] private Color _color;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetFollowObject(GameObject follow)
		{
			_follower.SetFolow(follow.transform);
		}

		#endregion


		#region private 関数

		private MiniMapObjectFollower _follower;
		private Material _material;

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_follower = gameObject.GetOrAddComponent<MiniMapObjectFollower>();

			var meshRenderer = GetComponent<MeshRenderer>();
			_material = new Material(_shaderContainer.GetOrCreateCache(ShaderName.SurfaceLightOff));
			_material.SetColor(ShaderProperty.Color, _color);

			meshRenderer.material = _material;
		}

		void OnDestroy()
		{
			GameObject.Destroy(_material);
		}

		#endregion
	}
}