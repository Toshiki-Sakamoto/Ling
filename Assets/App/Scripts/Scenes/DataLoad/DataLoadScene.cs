//
// DataLoadScene.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.29
//

using Ling.Common.Scene;
using UniRx;
using System;

namespace Ling.Scenes.DataLoad
{
	/// <summary>
	/// データ読み込みを担当する
	/// </summary>
	public class DataLoadScene : ExSceneBase
	{
		#region 定数, class, enum

		public enum Phase
		{
			Start,
		}
		
		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数
		
		#endregion


		#region プロパティ

		public override Common.Scene.SceneID SceneID => Common.Scene.SceneID.DataLoad;
		
		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Base> ScenePrepareAsync() =>
			Observable.Return(this);

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene()
		{
			//_phase.Add(Phase.Start, );
		}

		/// <summary>
		/// シーン終了時
		/// </summary>
		public override void StopScene() { }

		/// <summary>
		/// シーン遷移前に呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Unit> StopSceneAsync() =>
			Observable.Return(Unit.Default);

		#endregion


		#region private 関数

		/// <summary>
		/// データ読み込みをして問題なければバトルに遷移する
		/// </summary>
		private void ExecuteLoad()
		{
			// 実際にバトル関連の読み込みはBattleSceneが担当する
			// エラー処理もそっちで行う
		}

		#endregion
	}
}
