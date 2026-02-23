using Internal.Data.Player;
using Internal.Scripts.Core.Utils;
using Internal.Scripts.Gameplay.Collisions;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DoodleJumper : MonoBehaviour
    {
        [SerializeField] private TriggerLayerTouchingChecker _groundChecker;
        private Rigidbody2D _rigidbody;

        private PlayerConfiguration _playerConfiguration;

        private float _jumpForce => _playerConfiguration.JumpForce;

        [Inject]
        private void Construct(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            TryLogNullComponents();
        }

        private void TryLogNullComponents()
        {
            if (_playerConfiguration == null)
            {
                CustomDebugger.LogError(this, "Player configuration is null!" +
                                              "\nDid you forget to add installer on scene?", gameObject);
            }

            if (_groundChecker == null)
            {
                CustomDebugger.LogError(this, "ground checker field is null!", gameObject);
            }
        }

        private void FixedUpdate()
        {
            TryJump();
        }

        private void TryJump()
        {
            if (!_groundChecker.IsTouchingLayer)
                return;

            _rigidbody.linearVelocityY = 0;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

#if UNITY_EDITOR

        [Space(40), Header("Gizmos"), Tooltip("This config works only in edit mode, not in play mode")] 
        [SerializeField] private bool _drawMaxHeightGizmos = true;
        
        [ShowIf(nameof(_drawMaxHeightGizmos))]
        [SerializeField] private PlayerConfiguration _configToCalculateGizmos;

        private void OnDrawGizmos()
        {
            if (!_drawMaxHeightGizmos)
                return;
            
            if (_playerConfiguration == null && _configToCalculateGizmos == null)
                return;

            var rb = _rigidbody != null ? _rigidbody : GetComponent<Rigidbody2D>();
            if (rb == null)
                return;

            var config = _playerConfiguration != null ? _playerConfiguration : _configToCalculateGizmos;
            if (config == null)
                return;

            var jumpVelocity = config.JumpForce / rb.mass;
            var gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
            var maxHeight = (jumpVelocity * jumpVelocity) / (2f * gravity);

            var startPos = transform.position;
            var topPos = startPos + Vector3.up * maxHeight;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPos, topPos);
            Gizmos.DrawSphere(topPos, 0.05f);

            UnityEditor.Handles.Label(topPos, $"Max Height: {maxHeight:F2}");
        }

        private void Reset()
        {
            if (_rigidbody == null)
            {
                if (TryGetComponent<Rigidbody2D>(out var rb))
                {
                    _rigidbody = rb;
                }
            }
        }
#endif
    }
}