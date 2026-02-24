using System.Collections.Generic;
using Internal.Scripts.Core.Reactive;

namespace Internal.Scripts.Core.Data.Services
{
    public interface IPlayerDataService
    {
        public IReadOnlyList<GameHighScoreRecord> Records { get; }
        public ReactiveVariable<int> CurrentLevelScore { get; }
        
        public void ResetCurrentLevelHighScore();
        public bool TryChangeCurrentLevelScore(float currentLevelScore);
        public bool TryAddToRecordsCurrentLevelScore();
    }
}