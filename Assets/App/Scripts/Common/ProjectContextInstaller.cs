//
// ProjectContextInstaller.cs
// ProductName Ling
//
// Created by  on 2021.06.28
//

using Zenject;
using MessagePipe;

namespace Ling.Common
{
	public class ProjectContextInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			var option = Container.BindMessagePipe();

			Container.BindMessageBroker<NoticeEvent>(option);
			
			// GlobalMessagePipeを使用する前にSetProviderに設定する必要がある
			GlobalMessagePipe.SetProvider(Container.AsServiceProvider());
		}
	}
}
