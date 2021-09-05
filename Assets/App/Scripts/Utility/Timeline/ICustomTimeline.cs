//
// ITimelineExtension.cs
// ProductName Ling
//
// Created by  on 2021.08.22
//

using System;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Utility.Timeline
{
	/// <summary>
	/// タイムライン拡張のインターフェイス
	/// </summary>
	public interface ICustomTimeline :
		Utility.CustomBehaviour.ICustomComponent
	{
		UniTask PlayAsync(CancellationToken token);
	}
}
