using UnityEngine;
using Zenject;

namespace Ling.Map
{
	public class MapInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<Builder.Manager>()
				.AsSingle();
		}
	}
}