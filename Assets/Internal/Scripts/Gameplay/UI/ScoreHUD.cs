using System;
using Internal.Scripts.Core.Data.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        private IPlayerDataService _playerDataService;
        
        [Inject]
        private void Construct(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public void Initialize()
        {
            _playerDataService.CurrentLevelScore.OnValueChange += UpdateScore;
            UpdateScore(-1, _playerDataService.CurrentLevelScore.Value);
        }

        private void OnDestroy()
        {
            if (_playerDataService != null)
            {
                _playerDataService.CurrentLevelScore.OnValueChange -= UpdateScore;
            }
        }

        private void UpdateScore(int oldScore, int newScore)
        {
            _scoreLabel.text = $"Score: {newScore}";
        }
    }
}