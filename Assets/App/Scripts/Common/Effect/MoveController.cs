// 
// MoveController.cs  
// ProductName Ling
//  
// Created by  on 2021.08.24
// 

using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UniRx;

namespace Ling.Common.Effect
{
	/// <summary>
	/// 移動を制御する
	/// </summary>
	public class MoveController : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Vector3 _startPosition;
		[SerializeField] private Vector3 _endPosition;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public async UniTask Execute(float duration)
		{
			await transform.DOMoveX(1.0f, 1.0f);
			await transform.DOMove(_endPosition, duration);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour


		#endregion
	}
}