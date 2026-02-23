using Internal.Data.Player;
using Internal.Scripts.Core.Utils;
using Internal.Scripts.Gameplay.Collisions;
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
            TryLogNullComponents();

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void TryLogNullComponents()
        {
            if (_playerConfiguration == null)
            {
                CustomDebugger.LogError(this, "Player configuration is null!" +
                                              "\nDid you forget to add installer on scene?");
            }

            if (_groundChecker == null)
            {
                CustomDebugger.LogError(this, "ground checker field is null!");
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

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // TODO: Draw how high can jump (line)
        }
#endif
    }
}