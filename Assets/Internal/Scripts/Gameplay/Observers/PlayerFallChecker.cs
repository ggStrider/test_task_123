using Internal.Scripts.Gameplay.Player;
using Internal.Scripts.Installers.Signals;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Observers
{
    // i wanted to make this class pure (without monobehaviour), but i dont want
    // to bind it in zenject
    public class PlayerFallChecker : MonoBehaviour
    {
        private Camera _camera;
        
        private Transform _cameraTransform;
        private Transform _playerTransform;

        private SignalBus _signalBus;

        private bool _fell;

        private bool _initialized;
        
        [Inject]
        private void Construct(Camera playerCamera, PlayerDoodleController doodleController, SignalBus signalBus)
        {
            _camera = playerCamera;
            
            _cameraTransform = _camera.transform;
            _playerTransform = doodleController.transform;

            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _initialized = true;
        }

        public void LateUpdate()
        {
            if (!_initialized)
                return;
            
            if (_fell)
            {
                enabled = false; // no sense to use lifecycle again after player fell
                return;
            }
            
            var bottomOfScreen = _cameraTransform.position.y - _camera.orthographicSize;
            if (_playerTransform.position.y < bottomOfScreen)
            {
                _fell = true;
                FireSignal();
            }
        }

        private void FireSignal()
        {
            _signalBus.Fire<PlayerLoseSignal>(new());
        }
    }
}