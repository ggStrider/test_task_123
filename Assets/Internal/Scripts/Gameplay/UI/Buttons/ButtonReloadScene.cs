using Internal.Scripts.Core.Scenes;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public class ButtonReloadScene : BaseBoundButton
    {
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        protected override void ButtonClickAction()
        {
            _sceneLoader.ReloadScene();
        }
    }
}