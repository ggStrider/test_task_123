using System;
using Internal.Scripts.Gameplay.UI.Views;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI
{
    public class UIContainerOpenerOnAwake : MonoBehaviour
    {
        [SerializeField] private BaseUIPanel _baseUIPanel;
        private ViewsManager _viewsManager;
        
        [Inject]
        private void Construct(ViewsManager viewsManager)
        {
            _viewsManager = viewsManager;
        }

        private void Awake()
        {
            _viewsManager.OpenContainer(_baseUIPanel);
        }
    }
}