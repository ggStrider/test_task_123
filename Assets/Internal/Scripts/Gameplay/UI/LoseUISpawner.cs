using Internal.Scripts.Gameplay.UI.Views.InLevel;
using Internal.Scripts.Installers.Signals;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI
{
    public class LoseUISpawner : MonoBehaviour
    {
        [SerializeField] private LoseUI _losePrefabUI;

        private ViewsManager _viewsManager;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus, ViewsManager viewsManager)
        {
            _signalBus = signalBus;
            _viewsManager = viewsManager;
        }

        private void Awake()
        {
            _signalBus.Subscribe<PlayerLoseSignal>(OnPlayerFell);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerLoseSignal>(OnPlayerFell);
        }

        private void OnPlayerFell()
        {
            _viewsManager.OpenContainer(_losePrefabUI);
        }
    }
}