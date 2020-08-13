//
// AIInstaller.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.13
//

using Zenject;

namespace Ling.AI
{
	public class AIInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<AIManager>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}
