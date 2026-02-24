using System;
using System.Collections.Generic;
using Internal.Scripts.Core.Reactive;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Core.Data.Services
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly PlayerData _playerData;

        public IReadOnlyList<GameHighScoreRecord> Records => _playerData.Records.AsReadOnly();
        
        // imo needs also to be readonly value
        public ReactiveVariable<int> CurrentLevelScore => _playerData.CurrentLevelMaxHigh;

        [Inject]
        public PlayerDataService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void ResetCurrentLevelHighScore()
        {
            _playerData.CurrentLevelMaxHigh.Value = 0;
        }

        public bool TryChangeCurrentLevelScore(float currentLevelScore)
        {
            if (_playerData.CurrentLevelMaxHigh.Value >= currentLevelScore)
            {
                _playerData.CurrentLevelMaxHigh.Value = (int)currentLevelScore;
                return false;
            }

            return true;
        }

        public bool TryAddToRecordsCurrentLevelScore()
        {
            var thisLevelScore = _playerData.CurrentLevelMaxHigh.Value;
            if (thisLevelScore > 0 && !IsPlayerDataContainsThisScore(thisLevelScore))
            {
                var record = CreateRecordFrom(scoreValue: thisLevelScore);
                _playerData.Records.Add(record);

                return true;
            }

            return false;
        }

        #region Help tools

        private GameHighScoreRecord CreateRecordFrom(int scoreValue)
        {
            var record = new GameHighScoreRecord(
                score: scoreValue,
                whenScoreWasAchieved: DateTime.Today);

            return record;
        }

        private bool IsPlayerDataContainsThisScore(float scoreToCheck)
        {
            foreach (var record in Records)
            {
                if (Mathf.Approximately(record.Score, scoreToCheck))
                    return true;
            }

            return false;
        }

        #endregion
    }
}