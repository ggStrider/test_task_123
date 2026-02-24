using Cysharp.Threading.Tasks;
using Internal.Data.Scenes.Cards;
using Internal.Scripts.Core.Scenes.LoadingScreens;
using Internal.Scripts.Core.Utils;
using UnityEngine.SceneManagement;

namespace Internal.Scripts.Core.Scenes
{
    public class SceneLoader
    {
        private readonly ILoadingScreen _loadingScreen;
        
        public SceneLoader(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        private async UniTaskVoid LoadScene(string sceneName)
        {
            await _loadingScreen.FadeToLoadingScreen();

            var loadingScene = SceneManager.LoadSceneAsync(sceneName);
            if (loadingScene == null)
            {
                CustomDebugger.LogError(this, "Error while trying to load scene! Is scene name correct? Scene name: " + sceneName);
                return;
            }

            await loadingScene.ToUniTask();
            _loadingScreen.UnfadeFromLoadingScreen().Forget();
        }
        
        public void ReloadScene()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            LoadScene(currentSceneName).Forget();
        }

        public void LoadScene(SceneCard sceneCard)
        {
            LoadScene(sceneCard.SceneName).Forget();
        }
    }
}