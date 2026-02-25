using System;
using UnityEngine;

namespace Internal.Scripts.Core.Data
{
    [Serializable]
    public struct GameHighScoreRecord
    {
        public int Score;
        public string WhenScoreWasAchieved;

        public GameHighScoreRecord(int score, DateTime whenScoreWasAchieved)
        {
            Score = score;
            WhenScoreWasAchieved = whenScoreWasAchieved.ToString("O");
        }

        public DateTime GetDate() => DateTime.Parse(WhenScoreWasAchieved);

        public string GetFormatted()
        {
            var date = GetDate();
            return $"Score: {Score}. {date.Day}/{date.Month}/{date.Year}";
        }
    }
}