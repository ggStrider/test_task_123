using Internal.Scripts.Gameplay.Environment;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Views.InLevel
{
    public class PauseView : BaseUIPanel
    {
        private TimeManager _timeManager;
        
        [Inject]
        private void Construct(TimeManager timeManager)
        {
            _timeManager = timeManager;
        }
        
        private void OnEnable()
        {
            _timeManager.Pause();
        }

        private void OnDestroy()
        {
            _timeManager.Resume();
        }
    }
}