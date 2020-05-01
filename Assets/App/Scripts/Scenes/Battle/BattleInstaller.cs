using UnityEngine;
using Zenject;

namespace Ling.Scenes.Battle
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharaManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}