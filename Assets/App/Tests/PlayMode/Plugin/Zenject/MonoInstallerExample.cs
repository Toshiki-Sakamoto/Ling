using UnityEngine;
using Zenject;


namespace Ling.Tests.PlayMode.Plugin.ZenjectTest
{
	public class MonoInstallerExample : MonoInstaller
	{
		/// <summary>
		/// ‚±‚±‚ÅƒoƒCƒ“ƒh‚·‚é
		/// </summary>
		public override void InstallBindings()
		{
			Container.Bind<IExample>().To<Example>().AsSingle();

			// ˆá‚¤ID
			Container.Bind<IExampleIdTest>().WithId(ExampleIdTest.ID.First).To<ExampleIdTest>().AsCached();
			Container.Bind<IExampleIdTest>().WithId(ExampleIdTest.ID.Second).To<ExampleIdTest>().AsCached();
		}
	}
}