//
// CharaStatusValueObject.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.28
//


using System.Linq;
using UnityEngine;
using UniRx;

namespace Ling.Chara
{
	/// <summary>
	/// HPやスタミナなど、なにか足し合わせたり引いたりするステイタスを操作するValueObject
	/// </summary>
	[System.Serializable]
	public class CharaStatusValueObject
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private LongReactiveProperty _current = default;
		[SerializeField] private LongReactiveProperty _max = default;

		#endregion


		#region プロパティ

		public LongReactiveProperty Current => _current;
		public LongReactiveProperty Max => _max;

		#endregion


		#region コンストラクタ, デストラクタ

		public CharaStatusValueObject(long value, long maxValue)
		{
			_current = new LongReactiveProperty(value);
			_max = new LongReactiveProperty(maxValue);
		}

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 値を設定
		/// </summary>
		public void SetCurrent(long value) =>
			_current.SetValueAndForceNotify(value);

		/// <summary>
		/// 現在値に足し合わせる
		/// </summary>
		public void AddCurrent(long value)
		{
			// 最大値は上回らない
			SetCurrent(System.Math.Min(_current.Value + value, _max.Value));
		}
			
		/// <summary>
		/// 現在値から引く
		/// </summary>
		public void SubCurrent(long value) =>
			SetCurrent(System.Math.Max(_current.Value - value, 0));


		/// <summary>
		/// 値を設定
		/// </summary>
		public void SetMax(long value) =>
			_max.SetValueAndForceNotify(value);

		/// <summary>
		/// 現在値に足し合わせる
		/// </summary>
		public void AddMax(long value) =>
			SetMax(_max.Value + value);
			
		/// <summary>
		/// 現在値から引く
		/// </summary>
		public void SubMax(long value) =>
			SetMax(System.Math.Max(_max.Value - value, 0));

		#endregion


		#region private 関数

		#endregion
	}
}
