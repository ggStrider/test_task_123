using NaughtyAttributes;
using UnityEngine;

namespace Internal.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Camera))]
    public class CameraAspectFix : MonoBehaviour
    {
        [SerializeField] private float aspect = 0.462f;

        private Camera _camera;

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