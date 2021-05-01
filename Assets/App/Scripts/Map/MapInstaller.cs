using System.ComponentModel;
using UnityEngine;
using Zenject;

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
		}
	}
}