using System;
using System.Collections.Generic;
using Internal.Scripts.Core.Reactive;

namespace Internal.Scripts.Core.Data
{
    [Serializable]
    public class PlayerData
    {
        [NonSerialized] // we dont want to save it through different sessions
        public ReactiveVariable<int> CurrentLevelScore = new(0);
        
        public List<GameHighScoreRecord> Records = new();
    }
}