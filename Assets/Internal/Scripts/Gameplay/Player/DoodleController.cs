using Internal.Data.Player;
using Internal.Scripts.Core.Inputs;
using Internal.Scripts.Core.Utils;
using Internal.Scripts.Gameplay.Collisions;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DoodleController : MonoBehaviour
    {
        [SerializeField] private TriggerLayerTouchingChecker _groundChecker;
        
        private Rigidbody2D _rigidbody;
        private PlayerConfiguration _playerConfiguration;
        private InputReader _inputReader;
        
        private float _jumpForce => _playerConfiguration.JumpForce;

        [Inject]
        private void Construct(PlayerConfiguration playerConfiguration, InputReader inputReader)
        {
            _inputReader = inputReader;
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
            HandleGravity();
            Move();
            TryJump();
        }

        private void Move()
        {
            var tilt = _inputReader.Tilt.x;
            if (tilt != 0)
            {
                _rigidbody.AddForceX(_inputReader.Tilt.x * _playerConfiguration.MovementSensitivity,
                    ForceMode2D.Force);
            }
        }

        private void HandleGravity()
        {
            if (IsFalling())
            {
                _rigidbody.gravityScale = _playerConfiguration.FallingGravity;
            }
            else
            {
                _rigidbody.gravityScale = _playerConfiguration.OnJumpGravity;
            }
        }

        private void TryJump()
        {
            if (!_groundChecker.IsTouchingLayer)
                return;

            if (IsStillInJump())
                return;
           
            _rigidbody.linearVelocityY = 0;
            _rigidbody.AddForceY(_jumpForce, ForceMode2D.Impulse);
        }

        private bool IsStillInJump()
        {
            const float minIsJumpingVerticalVelocity = 0.1f;
            return _rigidbody.linearVelocityY >= minIsJumpingVerticalVelocity;
        }

        private bool IsFalling()
        {
            return _rigidbody.linearVelocityY < 0f;
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

            var rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                return;

            var config = Application.isPlaying
                ? _playerConfiguration
                : _configToCalculateGizmos;

            if (config == null)
                return;

            var jumpVelocity = config.JumpForce / rb.mass;
            var gravity = Mathf.Abs(Physics2D.gravity.y * config.OnJumpGravity);

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