//
// Base.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.15
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ling.Common.Scene
{
	/// <summary>
	/// シーンベース
	/// </summary>
	public abstract class Base : MonoBehaviour
	{
		#region 定数, class, enum

		/// <summary>
		/// 依存するシーンのデータを持つ
		/// </summary>
		public class DependenceData
		{
			public enum TimingType
			{
				Prev,   // シーン読み込み前のタイミングで読み込む
				Loaded, // シーン読み込み後のタイミングで読み込む
			}

			public TimingType Timing { get; set; }
			public SceneData Data { get; set; }


			public static DependenceData CreateAtPrev(SceneID sceneID, Argument argument = null) =>
				new DependenceData { Timing = TimingType.Prev, Data = new SceneData { SceneID = sceneID, Argument = argument } };

			public static DependenceData CreateAtLoaded(SceneID sceneID, Argument argument = null) =>
			   new DependenceData { Timing = TimingType.Loaded, Data = new SceneData { SceneID = sceneID, Argument = argument } };
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] protected DiContainer _diContainer;
		[Inject] protected Launcher _launcher;
		[Inject] protected Common.Scene.IExSceneManager _sceneManager = null;
		[Inject] protected Utility.IEventManager _eventManager = null;
		[Inject] protected Common.ProcessManager _processManager = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// シーン遷移時に渡される引数
		/// </summary>
		public Argument Argument { get; set; }

		/// <summary>
		/// Unityシーンインスタンス
		/// </summary>
		public UnityEngine.SceneManagement.Scene Scene { get; set; }

		/// <summary>
		/// Scene遷移時に生成、管理されるシーンデータ
		/// </summary>
		public SceneData SceneData { get; set; }

		/// <summary>
		/// StartScene呼び出されるときにtrueになる
		/// StopSceneでfalse
		/// </summary>
		public bool IsStartScene { get; set; }

		/// <summary>
		/// DIContainerを取得する
		/// </summary>
		public DiContainer DiContainer => _diContainer;

		/// <summary>
		/// 自分のシーンに必要なシーンID
		/// 自シーン読み込み後になければ読み込みを行う
		/// </summary>
		public virtual DependenceData[] Dependences => default(DependenceData[]);

		/// <summary>
		/// このシーンを親としてAddSceneされたもののシーンインスタンス
		/// </summary>
		public List<Base> Children { get; } = new List<Base>();

		/// <summary>
		/// AddSceneされた存在であれば親を持つ
		/// </summary>
		public Base Parent { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Base> ScenePrepareAsync() =>
			Observable.Return(this);

		/// <summary>
		/// 正規手順でシーンが実行されたのではなく
		/// 直接起動された場合StartSceneよりも前に呼び出される
		/// </summary>
		public virtual UniTask QuickStartSceneAsync() { return default(UniTask); }

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public virtual void StartScene() { }

		/// <summary>
		/// StartScene後呼び出される
		/// </summary>
		public virtual void UpdateScene() { }

		/// <summary>
		/// シーンが停止/一時中断される時
		/// </summary>
		public virtual void StopScene() { }

		/// <summary>
		/// シーンが削除される直前
		/// </summary>
		public virtual void DestroyScene() { }

		/// <summary>
		/// シーン停止時に呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Unit> StopSceneAsync() =>
			Observable.Return(Unit.Default);

		/// <summary>
		/// 自分を終了させる
		/// </summary>
		public void CloseScene() =>
			_sceneManager.CloseScene(this);

		/// <summary>
		/// 指定したシーンを直接起動する場合、必要な手続きを踏んでからシーン開始させる
		/// </summary>
		protected void QuickStart() =>
			_launcher.QuickStart(this);

		#endregion


		#region private 関数

		protected virtual void Awake()
		{
			_processManager?.SetupScene(this);

			// 起動済みなら何もしない
			if (!_launcher?.IsSceneBooted ?? false)
			{
				// シーンから直接起動した場合
				// 必要な初期化処理をしたあと起動する
				QuickStart();
			}
		}

		#endregion
	}


	public static class TimingTypeExtensions
	{
		public static bool IsLoaded(this Base.DependenceData.TimingType self) =>
			self == Base.DependenceData.TimingType.Loaded;

		public static bool IsPrev(this Base.DependenceData.TimingType self) =>
			self == Base.DependenceData.TimingType.Prev;
	}
}
