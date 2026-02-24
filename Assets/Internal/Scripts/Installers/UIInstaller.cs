using UnityEngine;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _viewsCanvas;

        public const string VIEWS_CANVAS_ID = "viewsCanvas";
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>()
                .WithId(VIEWS_CANVAS_ID)
                .FromInstance(_viewsCanvas)
                .AsSingle()
                .NonLazy();

            Container.Bind<ViewsManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}