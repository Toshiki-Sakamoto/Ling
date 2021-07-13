//
// ScopeTest.cs
// ProductName Ling
//
// Created by  on 2021.06.26
//

using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;
using MessagePipe;
using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;

namespace Ling.Tests.EditMode.Plugin.MessagePipeTest
{
	public class ScopeTest
	{
		#region 定数, class, enum

		public class Publisher
		{
			[Inject] private IPublisher<string> _publisher;

			public void Send(string message) =>
				_publisher.Publish(message);
		}

		public class Subscriber
		{
			[Inject] private ISubscriber<string> _subscriber;

			public void Setup(string name) =>
				_subscriber.Subscribe(x => Debug.Log($"{name} {x}"));
		}

		// 通知用
		public class NoticeEvent
		{
			public string Message;
		}

		/// <summary>
		/// 通知を送る
		/// </summary>
		public class NoticePublisher
		{
			[Inject] private IPublisher<NoticeEvent> _publisher;

			public void Send(NoticeEvent message) =>
				_publisher.Publish(message);
		}

		/// <summary>
		/// 通知を受ける
		/// </summary>
		public class NoticeSubscriber
		{
			[Inject] private ISubscriber<NoticeEvent> _subscriber;

			public void Setup() =>
				_subscriber.Subscribe(x => Debug.Log($"{x.Message}"));
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private DiContainer _container1;
		private DiContainer _container2;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
		}

		#endregion


		#region private 関数
		
		[Test]
		public void LocalScope()
		{
			_container1 = new DiContainer();
			_container2 = new DiContainer();
			
			_container1.BindMessageBroker<string>(_container1.BindMessagePipe());
			_container2.BindMessageBroker<string>(_container2.BindMessagePipe());

			// イベントを受ける方
			var service1 = _container1.Instantiate<Subscriber>();
			service1.Setup("Container1");
			
			var service2 = _container2.Instantiate<Subscriber>();
			service2.Setup("Container2");
			
			// イベントを投げる方
			var publisher1 = _container1.Instantiate<Publisher>();
			publisher1.Send("Test");
		}

		/// <summary>
		/// 環境全体にイベントを投げる場合
		/// </summary>
		[Test]
		public void GrobalScope()
		{
			_container1 = new DiContainer();
			_container2 = new DiContainer();
			
			// Grobalに設定する前に必要
			var option = _container1.BindMessagePipe();
			_container1.BindMessageBroker<string>(option);
			_container2.BindMessageBroker<string>(_container2.BindMessagePipe());
			
			// GlobalMessagePipeを使用する前にSetProviderに設定する必要がある
			GlobalMessagePipe.SetProvider(_container1.AsServiceProvider());
			
			var p = GlobalMessagePipe.GetPublisher<string>();
			var s = GlobalMessagePipe.GetSubscriber<string>();
			
			var d = s.Subscribe(x => Debug.Log(x));

			var service2 = _container2.Instantiate<Subscriber>();
			service2.Setup("Container2");

			p.Publish("10");
			p.Publish("20");

			// Disposeしたら購読ができなくなる
			d.Dispose();

			p.Publish("30");

			/*
			_container1.BindMessageBroker<string>(_container1.BindMessagePipe());
			_container2.BindMessageBroker<string>(_container2.BindMessagePipe());

			// イベントを受ける方
			var service1 = _container1.Instantiate<Subscriber>();
			service1.Setup("Container1");
			
			var service2 = _container2.Instantiate<Subscriber>();
			service2.Setup("Container2");
			
			// イベントを投げる方
			var publisher1 = _container1.Instantiate<Publisher>();
			publisher1.Send("Test");*/
		}

		#endregion
	}
}
