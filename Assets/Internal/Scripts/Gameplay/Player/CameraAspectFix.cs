using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Player
{
    public class CameraAspectFix : MonoBehaviour
    {
        [SerializeField] private float aspect = 1.33333f;

        private Camera _camera;

        [Inject]
        private void Construct(Camera playerCamera)
        {
            _camera = playerCamera;
        }

        private void Start()
        {
            UpdateCamera();
        }

        [Button]
        private void UpdateCamera()
        {
            if (_camera == null)
                _camera = GetComponent<Camera>();

            var orthographicSize = _camera.orthographicSize;

            _camera.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect, orthographicSize * aspect,
                -orthographicSize, orthographicSize,
                _camera.nearClipPlane, _camera.farClipPlane);
        }
    }
}