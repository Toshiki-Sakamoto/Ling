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

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private DiContainer _contaier1;
		private DiContainer _contaier2;

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
		public void Test1()
		{
			_contaier1 = new DiContainer();
			_contaier2 = new DiContainer();
			
			_contaier1.BindMessageBroker<string>(_contaier1.BindMessagePipe());
			_contaier2.BindMessageBroker<string>(_contaier2.BindMessagePipe());

			// イベントを受ける方
			var service1 = _contaier1.Instantiate<Subscriber>();
			service1.Setup("Container1");
			
			var service2 = _contaier2.Instantiate<Subscriber>();
			service2.Setup("Container2");
			
			// イベントを投げる方
			var publisher1 = _contaier1.Instantiate<Publisher>();
			publisher1.Send("Test");
		}

		#endregion
	}
}
