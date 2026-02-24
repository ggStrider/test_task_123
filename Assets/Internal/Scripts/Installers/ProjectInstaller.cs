using Internal.Scripts.Core.Data;
using Internal.Scripts.Core.Data.Services;
using Internal.Scripts.Core.Scenes;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerDataService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}