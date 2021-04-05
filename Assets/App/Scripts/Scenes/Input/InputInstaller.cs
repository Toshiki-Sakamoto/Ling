using UnityEngine;
using Zenject;
using Ling.Common.Input;


namespace Ling.Scenes.Input
{
	public class InputInstaller : MonoInstaller
	{
//		[SerializeField] private IInputProvider<InputControls.IMoveActions> _moveInputProvider = default;
//		[SerializeField] private IInputProvider<InputControls.IActionActions> _actionInputProvider = default;

		public override void InstallBindings()
		{
			/*
			Container
				.Bind<IInputProvider<InputControls.IMoveActions>>()
				.FromInstance(_moveInputProvider)
				.AsSingle();

			Container
				.Bind<IInputProvider<InputControls.IActionActions>>()
				.FromInstance(_actionInputProvider)
				.AsSingle();
				*/
		}
	}
}