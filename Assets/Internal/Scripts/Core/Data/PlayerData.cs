using System;
using System.Collections.Generic;
using Internal.Scripts.Core.Reactive;

namespace Internal.Scripts.Core.Data
{
    public class PlayerData
    {
        public ReactiveVariable<int> CurrentLevelScore = new(0);
        public List<GameHighScoreRecord> Records = new();
    }

    public struct GameHighScoreRecord
    {
        public int Score;
        public DateTime WhenScoreWasAchieved;

        public GameHighScoreRecord(int score, DateTime whenScoreWasAchieved)
        {
            Score = score;
            WhenScoreWasAchieved = whenScoreWasAchieved;
        }
        
        public string GetFormatted()
        {
            var ddmmyy = $"{WhenScoreWasAchieved.Day}/{WhenScoreWasAchieved.Month}/{WhenScoreWasAchieved.Year}";
            var formatted = $"Score: {Score}. {ddmmyy}";

            return formatted;
        }
    }
}