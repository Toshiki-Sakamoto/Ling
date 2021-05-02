// 
// HPView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.03
// 

using UnityEngine;
using UnityEngine.UI;

namespace Ling.Scenes.Status
{
	/// <summary>
	/// HPStatus View
	/// </summary>
	public class HPView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _text = default;
		[SerializeField] private Text _maxText = default;

		private long _max;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup(long max)
		{
			_max = max;
			SetHPText(max);
			SetHPMaxText(max);
		}

		public void SetHP(long current)
		{
			SetHPText(current);
		}
		public void SetHPMax(long max)
		{
			_max = max;
			SetHPMaxText(max);
		}

		#endregion


		#region private 関数


		private void SetHPText(long current)
		{
			_text.text = current.ToString();
		}
		private void SetHPMaxText(long max)
		{
			_maxText.text = max.ToString();
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
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