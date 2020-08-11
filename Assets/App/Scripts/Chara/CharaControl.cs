// 
// CharaControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.10
// 

using UnityEngine;
using System;
using System.Linq;
using UniRx;

namespace Ling.Chara
{
	/// <summary>
	/// キャラのModelとViewをつなげる役目と操作を行う
	/// </summary>
	public abstract class CharaControl<TModel, TView> : MonoBehaviour 
		where TModel : CharaModel
		where TView : Chara.ViewBase
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

        [SerializeField] private CharaStatus _status = default;
		[SerializeField] private TView _view = default;

		private TModel _model = default;

		#endregion


		#region プロパティ

		public TView View => _view;

		public TModel Model => _model;

		#endregion


		#region public, protected 関数

		public static CharaControl<TModel, TView> Create(TModel model, TView view)
		{
			var instance = view.gameObject.AddComponent<CharaControl<TModel, TView>>();

			return instance;
		}

		#endregion


		#region private 関数

		private void Setup(TModel model, TView view)
		{
            // 死亡時
            _status.IsDead.Where(isDead_ => isDead_)
                .Subscribe(_ =>
                {

                });
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}