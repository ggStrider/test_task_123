using Internal.Data.Scenes.Cards;
using Internal.Scripts.Core.Scenes;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public class ButtonLoadScene : BaseBoundButton
    {
        [SerializeField] private SceneCard _sceneCardToLoad;
        
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        protected override void ButtonClickAction()
        {
            _sceneLoader.LoadScene(_sceneCardToLoad);
        }
    }
}