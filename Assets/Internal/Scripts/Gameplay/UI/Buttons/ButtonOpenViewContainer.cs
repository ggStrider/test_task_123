using Internal.Scripts.Gameplay.UI.Views;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public class ButtonOpenViewContainer : BaseBoundButton
    {
        [SerializeField] private BaseUIPanel _panelToOpen;
        
        private ViewsManager _viewsManager;
        
        [Inject]
        private void Construct(ViewsManager viewsManager)
        {
            _viewsManager = viewsManager;
        }
        
        protected override void ButtonClickAction()
        {
            _viewsManager.OpenContainer(_panelToOpen);
        }
    }
}