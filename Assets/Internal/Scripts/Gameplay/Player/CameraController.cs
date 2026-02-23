using Internal.Scripts.Core.Extensions;
using Internal.Scripts.Core.Utils;
using UnityEngine;

namespace Internal.Scripts.Gameplay.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _followSpeed = 10f;

        // getting transform is taking some resources, because its using bridging
        // so its better to cache this variable before accessing in update methods
        private Transform _cameraTransform;

        private float _maxReachedVerticalPosition = float.NegativeInfinity;

        private void Awake()
        {
            if (_camera == null)
            {
                CustomDebugger.LogError(this, "No camera attached!", gameObject);
                return;
            }

            _cameraTransform = _camera.transform;
        }

        private void LateUpdate()
        {
            TryUpdateCameraPosition();
        }

        private void TryUpdateCameraPosition()
        {
            var yTarget = _followTarget.position.y;
            if (yTarget > _maxReachedVerticalPosition)
            {
                _maxReachedVerticalPosition = yTarget;
            }

            var interpolateT = Time.deltaTime * _followSpeed;
            var nextPosition = GetNextPosition();
            _camera.transform.position = Vector3.Lerp(_cameraTransform.position, nextPosition, interpolateT);
        }

        private Vector3 GetNextPosition()
        {
            var nextPosition = _cameraTransform.position.WithY(
                _maxReachedVerticalPosition);
            
            return nextPosition;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_camera == null)
            {
                if (TryGetComponent<Camera>(out var getCamera))
                {
                    _camera = getCamera;
                }
                else
                {
                    _camera = Camera.main;
                }
            }
        }
#endif
    }
}