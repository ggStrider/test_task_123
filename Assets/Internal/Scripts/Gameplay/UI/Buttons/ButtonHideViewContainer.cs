using Zenject;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public class ButtonHideViewContainer : BaseBoundButton
    {
        private ViewsManager _viewsManager;

        [Inject]
        private void Construct(ViewsManager viewsManager)
        {
            _viewsManager = viewsManager;
        }
        
        protected override void ButtonClickAction()
        {
            _viewsManager.HideCurrentContainer();
        }
    }
}