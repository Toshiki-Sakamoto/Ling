using UnityEngine;
using Zenject;

namespace Ling.Chara
{
    public class CharaInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Chara.CharaManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}