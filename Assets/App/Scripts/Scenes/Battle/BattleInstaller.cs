using UnityEngine;
using Zenject;

namespace Ling.Scenes.Battle
{
	public class BattleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<BattleScene>().FromComponentInHierarchy().AsSingle();
			Container.Bind<BattleView>().FromComponentInHierarchy().AsSingle();
			Container.Bind<BattleModel>().FromComponentInHierarchy().AsSingle();

			Container.Bind<BattleManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<PoolManager>().FromComponentInHierarchy().AsSingle();

			Container
				.Bind<Common.ProcessManager>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}