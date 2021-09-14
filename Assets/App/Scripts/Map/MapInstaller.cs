using System.ComponentModel;
using UnityEngine;
using Zenject;
using MessagePipe;

namespace Ling.Map
{
	public class MapInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<MapManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<Utility.ProcessManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<Common.Effect.IEffectManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			// イベント登録
			var option = Container.Resolve<MessagePipeOptions>();

			Container
				.BindMessageBroker<EventSpawnMapObject>(option);

			Container
				.BindMessageBroker<EventDestroyMapObject>(option);
		}
	}
}