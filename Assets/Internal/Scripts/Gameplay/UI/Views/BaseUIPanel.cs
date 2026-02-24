using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Views
{
    public class BaseUIPanel : MonoBehaviour
    {
        protected ViewsManager ViewsManager;
        
        [Inject]
        private void Construct(ViewsManager viewsManager)
        {
            ViewsManager = viewsManager;
        }
    }
}