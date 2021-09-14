//
// MiniMapObjectFollower.cs
// ProductName Ling
//
// Created by  on 2021.09.13
//

using UnityEngine;
using Sirenix.OdinInspector;

namespace Ling.Map
{
	/// <summary>
	/// Map上のオブジェクトをフォローしてMinimapオブジェクトに座標を反映させる
	/// </summary>
	public class MiniMapObjectFollower : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[ShowInInspector] private Transform _follow;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 追跡するものを設定する
		/// </summary>
		public void SetFolow(Transform follow)
		{
			_follow = follow;
		}

		#endregion


		#region private 関数

		private void LateUpdate()
		{
			if (_follow == null) return;

			var localPos = _follow.localPosition;

			transform.localPosition = new Vector3(localPos.x, localPos.y, -1.0f);
		}

		#endregion
	}
}
