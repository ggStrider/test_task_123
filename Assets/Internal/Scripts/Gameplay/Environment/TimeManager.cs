using UnityEngine;

namespace Internal.Scripts.Gameplay.Environment
{
    public class TimeManager
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}