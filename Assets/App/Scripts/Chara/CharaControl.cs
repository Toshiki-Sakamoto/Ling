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
	public interface ICharaController
	{
		CharaModel Model { get; }
		
		ViewBase View { get; }
	}

	/// <summary>
	/// キャラのModelとViewをつなげる役目と操作を行う
	/// </summary>
	public abstract class CharaControl<TModel, TView> : MonoBehaviour, ICharaController
		where TModel : CharaModel
		where TView : ViewBase
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

		
		public TModel Model => _model;

		public TView View => _view;

		// ICharaController
		CharaModel ICharaController.Model => _model;
		ViewBase ICharaController.View => _view;

		#endregion


		#region public, protected 関数

		public void Setup(TModel model)
		{
			_model = model;
			_status = model.Status;
			
            // 死亡時
            _status.IsDead.Where(isDead_ => isDead_)
                .Subscribe(_ =>
                {
					// Viewにも伝える
                });
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		#endregion
	}
}