// 
// ItemView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.07
// 

using UnityEngine;

namespace Ling.Item
{
	/// <summary>
	/// アイテムの見た目を管理
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public class ItemView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Const.Item.Category _category = default;
		//		[SerializeField] private Animator _animator = default; // 使わないかな

		private SpriteRenderer _renderer = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup()
		{

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
			_renderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}