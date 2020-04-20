using UnityEngine;
using Zenject;


namespace Ling.Map.Builder.Split
{
	public class SplitInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindFactory<Half.Splitter, Half.Splitter.Factory>();
			
			Container
				.BindFactory<ISplitter, SplitBuilderFactory>()
				.FromFactory<CustomSplitBuilderFactory>();
		}
	}
}