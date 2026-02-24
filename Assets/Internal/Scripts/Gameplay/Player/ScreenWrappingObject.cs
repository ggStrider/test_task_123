using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScreenWrappingObject : MonoBehaviour
    {
        private Camera _camera;

        private float _leftBound;
        private float _rightBound;
        private float _halfWidth;

        // better to cache transform because of bridging
        private Transform _transform;

        [Inject]
        private void Construct(Camera playerCamera)
        {
            _camera = playerCamera;
        }

        public void Initialize()
        {
            _transform = transform;
            
            var screenHalfWidth = _camera.orthographicSize * _camera.aspect;
            var camX = _camera.transform.position.x;

            _leftBound = camX - screenHalfWidth;
            _rightBound = camX + screenHalfWidth;

            _halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        }

        private void LateUpdate()
        {
            var pos = _transform.position;

            if (pos.x > _rightBound + _halfWidth)
                pos.x = _leftBound - _halfWidth;
            else if (pos.x < _leftBound - _halfWidth)
                pos.x = _rightBound + _halfWidth;

            _transform.position = pos;
        }
    }
}