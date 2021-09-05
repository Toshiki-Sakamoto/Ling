//
// EffectPool.cs
// ProductName Ling
//
// Created by  on 2021.09.02
//

using Ling.Const;

namespace Ling.Common.Effect
{
	/// <summary>
	/// エフェクトプール
	/// </summary>
	public class EffectPool : Utility.Pool.PoolManager<Const.EffectType, EffectPool>
	{
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        protected override void CallNotExistsPool(EffectType key, string id)
        {
            // idがファイル名なのでそのまま生成する
        }

        #endregion


        #region private 関数

        #endregion
    }
}
