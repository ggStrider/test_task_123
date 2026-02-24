using Zenject;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public class ButtonBackContainer : BaseBoundButton
    {
        private ViewsManager _viewsManager;
        
        [Inject]
        private void Construct(ViewsManager viewsManager)
        {
            _viewsManager = viewsManager;
        }
        
        protected override void ButtonClickAction()
        {
            _viewsManager.OpenPreviousContainer();
        }
    }
}