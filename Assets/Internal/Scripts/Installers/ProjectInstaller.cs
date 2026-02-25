using Internal.Scripts.Core.Data;
using Internal.Scripts.Core.Data.Services;
using Internal.Scripts.Core.Data.Services.Saves;
using Internal.Scripts.Core.Scenes;
using Internal.Scripts.Core.Scenes.LoadingScreens;
using Internal.Scripts.Gameplay.Environment;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SaveService>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerDataService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<SceneLoader>()
                .FromMethod(CreateSceneLoader)
                .AsSingle()
                .NonLazy();

            Container.Bind<TimeManager>()
                .AsSingle()
                .NonLazy();
        }

        private SceneLoader CreateSceneLoader()
        {
            return new SceneLoader(_loadingScreen);
        }
    }
}