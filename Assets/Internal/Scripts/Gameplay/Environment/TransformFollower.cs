using System;
using UnityEngine;

namespace Internal.Scripts.Gameplay.Environment
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _whoToFollow;
        [SerializeField] private bool _ignoreZ = true;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            var nextPosition = _whoToFollow.position;
            if (_ignoreZ)
                nextPosition.z = 0;
            
            _transform.position = nextPosition;
        }
    }
}