using DG.Tweening;
using UnityEngine;

namespace Internal.Scripts.Gameplay.Environment
{
    public class EndlessRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationDirection = new(0, 0, 360);
        [SerializeField] private float _duration = 2f;

        [Space] 
        [SerializeField] private bool _ignoreTimeScale = true;

        private void OnEnable()
        {
            transform.DOKill();

            transform
                .DORotate(_rotationDirection, _duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental)
                .SetUpdate(_ignoreTimeScale);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}