using UnityEngine;
using Zenject;


namespace Ling.Common
{
	public class CommonInstaller : MonoInstaller
	{
#if DEBUG
		[SerializeField] private Transform _debugConfigManagerRoot = default;
		[SerializeField] private DebugConfig.DebugConfigManager _debugConfigManager = default;
#endif

		public override void InstallBindings()
		{
			Container
				.Bind<Launcher>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<Scene.IExSceneManager>()
				.To<Scene.ExSceneManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<MasterData.MasterManager>()
				.FromComponentInHierarchy()
				.AsSingle();

#if DEBUG
			var debugManagerInstance = Container.InstantiatePrefabForComponent<DebugConfig.DebugConfigManager>(_debugConfigManager, _debugConfigManagerRoot);
			Container
				.Bind<DebugConfig.DebugConfigManager>()
				.FromInstance(debugManagerInstance)
				.AsSingle();
#endif
		}
	}
}