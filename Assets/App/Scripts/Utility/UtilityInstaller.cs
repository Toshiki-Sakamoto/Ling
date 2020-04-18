using UnityEngine;
using Zenject;

namespace Ling.Utility
{
	public class UtilityInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IEventManager>()
				.To<EventManager>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}