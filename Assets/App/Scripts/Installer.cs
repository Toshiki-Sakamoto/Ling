using Zenject;

public class Installer : MonoInstaller
{
	public override void InstallBindings()
	{
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

#if DEBUG
		
		Container
			.BindInterfacesAndSelfTo<Ling._Debug.DebugRootMenuData>()
			.AsSingle();

#endif
	}
}