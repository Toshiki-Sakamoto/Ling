using UnityEngine;
using Zenject;

namespace Ling.Map.Builder
{
	public class BuilderInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IManager>()
				.To<Manager>()
				.AsSingle();
		}
	}
}