using UnityEngine;
using Zenject;
using MessagePipe;

namespace Ling.Chara
{
	public class CharaInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<Chara.CharaManager>()
				.FromComponentInHierarchy().AsSingle();

			// イベント登録
			var option = Container.BindMessagePipe();

			Container
				.BindMessageBroker<Chara.EventKilled>(option);


			Container
				.Bind<Skill.ISkillCalculater>()
				.To<Skill.SkillCalculater>()
				.AsSingle();
		}
	}
}