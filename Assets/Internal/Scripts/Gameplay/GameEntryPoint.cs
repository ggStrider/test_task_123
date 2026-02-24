using System;
using Internal.Scripts.Core.Data.Services;
using Internal.Scripts.Gameplay.Observers;
using Internal.Scripts.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private ScreenWrappingObject _playerScreenWrapping;
        [SerializeField] private PlayerFallChecker _playerFallChecker;
        
        private PlayerDoodleController _playerDoodleController;
        private PlayerDataService _playerDataService;
        private CameraController _cameraController;

        [Inject]
        private void Construct(PlayerDoodleController doodleController, PlayerDataService dataService,
            CameraController cameraController)
        {
            _playerDoodleController = doodleController;
            _playerDataService = dataService;
            _cameraController = cameraController;
        }

        private void Awake()
        {
            _playerDataService.ResetCurrentLevelHighScore();

            _playerDoodleController.Initialize();
            _cameraController.Initialize();
            
            _playerScreenWrapping.Initialize();
            
            _playerFallChecker.Initialize();
        }
    }
}