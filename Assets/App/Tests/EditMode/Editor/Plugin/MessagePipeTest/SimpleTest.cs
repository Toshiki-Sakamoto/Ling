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
			// Zenjectによって自動的にInject
			[Inject] private IPublisher<string> _pubLisher;

			public void Send(string message)
			{
				_pubLisher.Publish(message);
			}
		}

		public class MessageHub : System.IDisposable
		{
			// Zenjectによって自動的にInject
			[Inject] private ISubscriber<string> _subscriber;
			
			private System.IDisposable disposable;

			public void Setup()
			{
				var bag = DisposableBag.CreateBuilder();
				
				_subscriber.Subscribe(x => Debug.Log(x)).AddTo(bag);
				
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
			//Configure(_contaier);
		}

		#endregion


		#region private 関数

		[Test]
		public void Test1()
		{
			InstallBindings(_contaier);
			
			var hub = _contaier.Instantiate<MessageHub>();
			hub.Setup();
			
			var service = _contaier.Instantiate<MessageService>();
			
			service.Send("Test");
		}

		private void Configure(DiContainer builder)
		{
			var options = builder.BindMessagePipe();
			
			//builder.BindMessageHandlerFilter<MyFilter<int>>();
			
			//GlobalMessagePipe.SetProvider(builder.AsServiceProvider());
		}

		private void InstallBindings(DiContainer builder)
		{
			var options = builder.BindMessagePipe();

			// 使用するためにはDIContainerにバインドをしなければならない
			builder.BindMessageBroker<string>(options);
		}

		#endregion
	}
}
