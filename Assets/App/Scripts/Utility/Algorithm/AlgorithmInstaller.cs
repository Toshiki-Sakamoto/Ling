using UnityEngine;
using Zenject;

namespace Utility.Algorithm
{
	public class AlgorithmInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<Search>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}