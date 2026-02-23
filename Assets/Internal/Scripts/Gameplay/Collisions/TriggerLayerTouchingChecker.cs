using NaughtyAttributes;
using UnityEngine;

namespace Internal.Scripts.Gameplay.Collisions
{
    public class TriggerLayerTouchingChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerToCheckIsTouching;
        [SerializeField] private Collider2D _collider;

        [ReadOnly] [SerializeField] private bool _isTouchingLayer;

        public bool IsTouchingLayer => _isTouchingLayer;

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layerToCheckIsTouching);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layerToCheckIsTouching);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_collider == null)
            {
                if (TryGetComponent<Collider2D>(out var gotCollider))
                {
                    _collider = gotCollider;
                }

                _collider.isTrigger = true;
            }
        }
#endif
    }
}