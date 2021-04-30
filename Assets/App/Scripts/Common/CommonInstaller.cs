using System.Security.AccessControl;
using UnityEngine;
using Zenject;

namespace Ling.Common
{
	public class CommonInstaller : MonoInstaller
	{
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
				.Bind<Input.IInputManager>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}