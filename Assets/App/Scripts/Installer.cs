using Zenject;

public class Installer : MonoInstaller<Installer>
{
	public override void InstallBindings()
	{
#if DEBUG
		// Debug内部のDiContainerへのInjectを通すためにToでタイプを宣言してZenjectに生成してもらう
		Container
			.Bind(typeof(Utility.DebugConfig.DebugRootMenuData), typeof(IInitializable))
			.To<Ling._Debug.DebugRootMenuData>()
			.AsSingle();

#endif

		Container
			.Bind<Utility.MasterData.IMasterManager>()
			.FromComponentInHierarchy()
			.AsSingle();

		Container
			.Bind<Ling.MasterData.IMasterHolder>()
			.FromComponentInHierarchy()
			.AsSingle();

		Container
			.Bind<Utility.UserData.IUserDataManager>()
			.FromComponentInHierarchy()
			.AsSingle();

		Container
			.Bind<Ling.UserData.IUserDataHolder>()
			.FromComponentInHierarchy()
			.AsSingle();
	}
}