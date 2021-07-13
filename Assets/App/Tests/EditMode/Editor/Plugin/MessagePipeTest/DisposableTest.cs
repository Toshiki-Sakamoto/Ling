//
// DisposableTest.cs
// ProductName Ling
//
// Created by  on 2021.06.29
//

using NUnit.Framework;
using MessagePipe;
using Zenject;
using UnityEngine;
using System;

namespace Ling.Tests.EditMode.Plugin.MessagePipeTest
{
	public class DisposableTest
	{
		// イベント送る方
		public class Publisher
		{
			[Inject] private IPublisher<MyEvent> _publisher;

			public void Send(MyEvent ev) =>
				_publisher.Publish(ev);
		}

		// イベント受け取る方
		public class Subscriber : IDisposable
		{
			private readonly IDisposable _disposable;

			public Subscriber(ISubscriber<MyEvent> subscriber)
			{
				var bag = DisposableBag.CreateBuilder();

				subscriber
					.Subscribe(x => Debug.Log($"{x.Message}"))
					.AddTo(bag);

				_disposable = bag.Build();
			}

			void IDisposable.Dispose()
            {
				_disposable.Dispose();
			}
		}

		// 送るイベント
		public class MyEvent 
		{ 
			public string Message; 
		}


		private DiContainer _container;

		/// <summary>
		/// SubscribeのIDosposableをかならず何らかの形でハンドリングする必要がある。
		/// しなければリークする
		/// </summary>
		[Test]
		public void イベントを破棄する()
		{
			_container = new DiContainer();

			var option = _container.BindMessagePipe();
			_container.BindMessageBroker<MyEvent>(option);

			// イベントを受ける方
			using (_container.Instantiate<Subscriber>())
			{
				// イベントを投げる方
				var publisher = _container.Instantiate<Publisher>();
				publisher.Send(new MyEvent { Message = "テストメッセージ" });
			}
		}
	}
}
