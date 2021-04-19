//
// AddressableHelper.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.16
//

namespace Utility.AssetBundle
{
	/// <summary>
	/// AAB関連のヘルパクラス
	/// </summary>
	public class AddressableHelper
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

#if UNITY_EDITOR
		/// <summary>
		/// Editor外からデバッグのためにローカルアセットを使用したい時に使用される
		/// </summary>
		public static System.Func<string, UnityEngine.Object> AssetObjectGetter;
#endif

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

#if UNITY_EDITOR
		/// <summary>
		/// 同期的にファイルを読み込む（Editor専用
		/// </summary>
		public static TObject LoadAsset<TObject>(string address) where TObject : UnityEngine.Object
		{
			return AssetObjectGetter(address) as TObject;
		}
#endif

		#endregion


		#region private 関数

		#endregion
	}
}
