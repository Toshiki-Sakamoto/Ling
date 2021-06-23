//
// SInmpleTest.cs
// ProductName Ling
//
// Created by  on 2021.06.22
//

using NUnit.Framework;
using Zenject;
using Assert = UnityEngine.Assertions.Assert;
using MessagePipe;
using UnityEngine;

namespace Ling.Tests.EditMode.Plugin.MessagePipeTest
{
	/// <summary>
	/// 
	/// </summary>
	public class SimpleTest
	{
		#region 定数, class, enum

		public class MessageService
		{
			private IPublisher<string> _pubLisher;

			public MessageService(IPublisher<string> publisher)
			{
				_pubLisher = publisher;
			}

			public void Send(string message)
			{
				_pubLisher.Publish(message);
			}
		}

		public class MessageHub : System.IDisposable
		{
			private readonly System.IDisposable disposable;
		
			public MessageHub(ISubscriber<string> subscriber)
			{
				var bag = DisposableBag.CreateBuilder();
				
				subscriber.Subscribe(x => Debug.Log(x)).AddTo(bag);
				
				disposable = bag.Build();
			}

			void System.IDisposable.Dispose()
			{
				disposable.Dispose();
			}
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数
		
		private DiContainer _contaier;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
			_contaier = new DiContainer();
			Configure(_contaier);
		}

		#endregion


		#region private 関数

		[Test]
		public void Test1()
		{
			var hub = _contaier.Instantiate<MessageHub>();
			var service = _contaier.Instantiate<MessageService>();
			
			service.Send("Test");
		}

		private void Configure(DiContainer builder)
		{
			var options = builder.BindMessagePipe();
			
			builder.BindMessageBroker<string>(options);
			
			//builder.BindMessageHandlerFilter<MyFilter<int>>();
			
			GlobalMessagePipe.SetProvider(builder.AsServiceProvider());
		}

		#endregion
	}
}
