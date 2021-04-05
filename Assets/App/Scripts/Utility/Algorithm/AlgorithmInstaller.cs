using UnityEngine;
using Zenject;

namespace Ling.Utility.Algorithm
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