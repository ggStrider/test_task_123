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
        public ReactiveVariable<int> CurrentLevelScore => _playerData.CurrentLevelScore;

        [Inject]
        public PlayerDataService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void ResetCurrentLevelHighScore()
        {
            _playerData.CurrentLevelScore.Value = 0;
        }

        public bool TryChangeCurrentLevelScore(float newLevelScore)
        {
            if (_playerData.CurrentLevelScore.Value < newLevelScore)
            {
                _playerData.CurrentLevelScore.Value = (int)newLevelScore;
                return true;
            }

            return false;
        }

        public bool TryAddToRecordsCurrentLevelScore()
        {
            var thisLevelScore = _playerData.CurrentLevelScore.Value;
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