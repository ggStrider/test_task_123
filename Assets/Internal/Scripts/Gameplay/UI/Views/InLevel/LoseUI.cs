using System;
using Internal.Scripts.Core.Data.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Views.InLevel
{
    public class LoseUI : BaseUIPanel
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        
        private IPlayerDataService _playerDataService;
        
        [Inject]
        private void Construct(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        private void OnEnable()
        {
            _scoreLabel.text = $"SCORE: {_playerDataService.CurrentLevelScore.Value}";
        }
    }
}