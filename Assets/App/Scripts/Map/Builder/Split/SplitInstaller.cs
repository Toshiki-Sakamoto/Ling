using UnityEngine;
using Zenject;


namespace Ling.Map.Builder.Split
{
	public class SplitInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindFactory<Half.Splitter, Half.Splitter.Factory>();

			Container
				.BindFactory<ISplitter, SplitBuilderFactory>()
				.FromFactory<CustomSplitBuilderFactory>();


			Container
				.BindFactory<Road.SimpleRoadBuilder, Road.SimpleRoadBuilder.Factory>();

			Container
				.BindFactory<SplitConst.RoadBuilderType, Road.ISplitRoadBuilder, Road.SplitRoadBuilderFactory>()
				.FromFactory<Road.CustomSplitRoadBuilderFactory>();
		}
	}
}