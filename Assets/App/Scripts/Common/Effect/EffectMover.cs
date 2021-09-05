// 
// EffectMover.cs  
// ProductName Ling
//  
// Created by  on 2021.08.30
// 

using UnityEngine;
using Utility.Timeline;
using System;
using DG.Tweening;
using Utility.CustomBehaviour;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Ling.Common.Effect
{

    public interface IEffectMoveCore
    {
        void SetStartPos(in Vector3 pos);
        void SetEndPos(in Vector3 pos);

        void SetSpeed(float speed);

        UniTask PlayAsync(Transform transform, CancellationToken token);
    }

    /// <summary>
	/// 等速で直線に動く
	/// </summary>
    public class EffectMoveCoreConstantLiner : IEffectMoveCore
    {
		private Vector3 _startPos, _endPos;
		private float _speed;

        void IEffectMoveCore.SetStartPos(in Vector3 pos) =>
            _startPos = pos;

        void IEffectMoveCore.SetEndPos(in Vector3 pos) =>
            _endPos = pos;

        void IEffectMoveCore.SetSpeed(float speed) =>
            _speed = speed;


        async UniTask IEffectMoveCore.PlayAsync(Transform transform, CancellationToken token)
        {
			var duration = Vector3.Distance(_endPos, _startPos) / _speed;

			transform.position = _startPos;
			await transform.DOMove(_endPos, duration).WithCancellation(token);
        }
    }


    public interface IEffectMover : ICustomTimeline
    {
        void RegistrCore(IEffectMoveCore core);
    }

	/// <summary>
	/// エフェクトを移動させる
	/// </summary>
	public class EffectMover : Utility.CustomBehaviour.AbstractCustomBehaviour,
        IEffectMover
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        private IEffectMoveCore _core;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        public override void Register(ICustomBehaviourCollection owner)
        {
            base.Register(owner);

            owner.AddCustomComponent<IEffectMover>(this);
        }

        async UniTask ICustomTimeline.PlayAsync(CancellationToken token)
		{
            await _core.PlayAsync(transform, token);
		}


        void IEffectMover.RegistrCore(IEffectMoveCore core)
        {
            _core = core;
        }

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour


        #endregion
    }
}