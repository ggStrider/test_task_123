using Internal.Data.Player;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfiguration>()
                .FromScriptableObject(_playerConfiguration)
                .AsSingle()
                .NonLazy();
        }
    }
}