//
// SimpleTest2.cs
// ProductName Ling
//
// Created by  on 2021.06.29
//

using NUnit.Framework;
using MessagePipe;
using Zenject;
using UnityEngine;


namespace Ling.Tests.EditMode.Plugin.MessagePipeTest
{
	public class Test
	{
		// イベント送る方
		public class Publisher
		{
			[Inject] private IPublisher<MyEvent> _publisher;

			public void Send(MyEvent ev) =>
				_publisher.Publish(ev);
		}

		// イベント受け取る方
		public class Subscriber
		{
			[Inject] private ISubscriber<MyEvent> _subscriber;

			public void Setup() =>
				_subscriber.Subscribe(x => Debug.Log($"{x.Message}"));
		}

		// 送るイベント
		public class MyEvent 
		{ 
			public string Message; 
		}


		private DiContainer _container;

		[Test]
		public void SimpleTest()
		{
			_container = new DiContainer();
				
			_container.BindMessageBroker<MyEvent>(_container.BindMessagePipe());

			// イベントを受ける方
			var sub = _container.Instantiate<Subscriber>();
			sub.Setup();
			
			// イベントを投げる方
			var publisher = _container.Instantiate<Publisher>();
			publisher.Send(new MyEvent { Message = "テストメッセージ" });
		}
	}
}
