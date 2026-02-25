using System;
using System.Collections.Generic;
using Internal.Scripts.Core.Reactive;

namespace Internal.Scripts.Core.Data
{
    [Serializable]
    public class PlayerData
    {
        public ReactiveVariable<int> CurrentLevelScore = new(0);
        public List<GameHighScoreRecord> Records = new();
    }
}