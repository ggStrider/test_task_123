using Internal.Data.Player;
using Internal.Scripts.Core.Inputs;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private Camera _playerCamera;
        
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
        }
    }
}