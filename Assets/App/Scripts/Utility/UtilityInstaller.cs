using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Utility
{
	public class UtilityInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<SaveData.ISaveDataHelper>()
				.To<SaveData.SaveHelper>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<IEventManager>()
				.To<EventManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<AssetBundle.AssetBundleManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<UI.CanvasCategoryManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<UtilityInitializer>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<ProcessManager>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<ShaderEx.IShaderContainer>()
				.FromInstance(new ShaderEx.ShaderContainer())
				.AsSingle();
		}
	}
}