using Zenject;

public class Installer : MonoInstaller
{
	public override void InstallBindings()
	{
		Container
			.Bind<Ling.Common.MasterData.IMasterManager>()
			.FromComponentInHierarchy()
			.AsSingle();

		Container
			.Bind<Ling.MasterData.IMasterHolder>()
			.FromComponentInHierarchy()
			.AsSingle();
	}
}