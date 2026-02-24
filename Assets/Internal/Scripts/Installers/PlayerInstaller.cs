using Internal.Data.Player;
using Internal.Scripts.Core.Inputs;
using Internal.Scripts.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private PlayerDoodleController _playerDoodleController;
        [SerializeField] private CameraController _cameraController;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputReader>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerConfiguration>()
                .FromScriptableObject(_playerConfiguration)
                .AsSingle()
                .NonLazy();

            Container.Bind<Camera>()
                .FromInstance(_playerCamera)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerDoodleController>()
                .FromInstance(_playerDoodleController)
                .AsSingle()
                .NonLazy();

            Container.Bind<CameraController>()
                .FromInstance(_cameraController)
                .AsSingle()
                .NonLazy();
        }
    }
}