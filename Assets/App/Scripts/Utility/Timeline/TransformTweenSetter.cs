// 
// EffectMovable.cs  
// ProductName Ling
//  
// Created by  on 2021.08.15
// 

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utility.Timeline
{
	/// <summary>
	/// Transform Tween Clipの開始と終了地点を動かすことの出来るスクリプト
	/// </summary>
	public class TransformTweenSetter : MonoBehaviour, ICustomTimeline
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _startTransform;
		[SerializeField] private Transform _endTransform;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		UniTask ICustomTimeline.PlayAsync(CancellationToken token)
		{
			return default(UniTask);
		}

		/// <summary>
		/// スキル再生位置
		/// </summary>
		public void SetStartPosition(Vector3 pos)
		{
			_startTransform.position = pos;
		}

		/// <summary>
		/// スキル終了位置
		/// </summary>
		public void SetEndPosition(Vector3 pos)
		{
			_endTransform.position = pos;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			// 親にTimelinePlayerがあるならばそこに入れ込む
			var player = transform.GetComponentInParent<TimelinePlayer>();
			if (player != null)
			{
//				player.AddCustom(this);
			}
		}

		#endregion
	}
}