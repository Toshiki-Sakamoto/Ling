// 
// EnemyControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.10
// 

using UnityEngine;
using UniRx;
using System;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;

namespace Ling.Chara
{
	/// <summary>
	/// Enemy Control
	/// </summary>
	public class EnemyControl : CharaControl<EnemyModel, EnemyView>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		/// <summary>
		/// 削除時に呼び出される
		/// </summary>
		public IObserver<EnemyControl> OnDestroyed;

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		// todo: 敵用の経験値コントローラーを作る
		public override Exp.ICharaExpController ExpController => default(Exp.ICharaExpController);

		#endregion


		#region public, protected 関数

		protected override async UniTask ThinkActionStartAsync()
		{
			// データ内からどのスキルを選択するか選ばせて良いかも
			// 今は最初のもの
			var skillMaster = _model.Master.AttackAIData.FirstSkillMaster;
			
			// 帰ってくるまで待機する

			await _model.ActionThinkCore.ThinkStartAsync(skillMaster);
		}

		protected override void DestroyProcessInternal()
		{
			OnDestroyed?.OnNext(this);
			OnDestroyed?.OnCompleted();
		}

		public void Resume()
		{
			// 自分のGameOobject全体をInjectする
			_diContainer.InjectGameObject(gameObject);
			
			// データから読み込み時、プールオブジェクトに対してLoadIntoしているのでセーブ時のGameObjectインスタンスではない。
			// そのためModelインスタンスは取り直す必要がある
			_model = GetComponent<EnemyModel>();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}