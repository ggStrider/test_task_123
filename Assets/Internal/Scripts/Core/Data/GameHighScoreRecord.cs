using System;

namespace Internal.Scripts.Core.Data
{
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