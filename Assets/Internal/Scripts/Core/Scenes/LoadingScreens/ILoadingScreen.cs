using Cysharp.Threading.Tasks;

namespace Internal.Scripts.Core.Scenes.LoadingScreens
{
    public interface ILoadingScreen
    {
        public UniTask FadeToLoadingScreen();
        public UniTask UnfadeFromLoadingScreen();
    }
}