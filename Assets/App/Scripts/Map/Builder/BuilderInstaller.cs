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
				.FromNew()
				.AsSingle();

			Container
				.BindFactory<Const.BuilderType, IBuilder, BuilderFactory>()
				.FromFactory<CustomBuilderFactory>();
		}
	}
}