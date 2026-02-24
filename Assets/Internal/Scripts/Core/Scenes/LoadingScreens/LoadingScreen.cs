using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Internal.Scripts.Core.Scenes.LoadingScreens
{
    [RequireComponent(typeof(Canvas))]
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private GameObject _loadingScreenParent;

        public UniTask FadeToLoadingScreen()
        {
            _loadingScreenParent.SetActive(true);
            return default;
        }

        public UniTask UnfadeFromLoadingScreen()
        {
            _loadingScreenParent.SetActive(false);
            return default;
        }
    }
}