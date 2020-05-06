using UnityEngine;
using Zenject;

namespace Ling.Adv
{
    public class AdvInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Manager>()
                .FromComponentInHierarchy().AsSingle();

            Container
                .Bind<Engine.Manager>()
                .FromComponentInHierarchy().AsSingle();
        }
    }
}