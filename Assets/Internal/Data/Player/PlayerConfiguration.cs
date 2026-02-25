using UnityEngine;

namespace Internal.Data.Player
{
    [CreateAssetMenu(fileName = "New Player Configuration", menuName = "Game Jumper/Player/Configuration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [field: SerializeField] public float MovementSensitivity { get; private set; } = 10f;
        [field: SerializeField] public float JumpForce { get; private set; } = 10f;
        
        [field: SerializeField] public float MaxHorizontalSpeed { get; private set; } = 5f;
        [field: SerializeField] public float HorizontalDamping { get; private set; } = 15f;
        [field: SerializeField] public float DirectionChangeFactor { get; private set; } = 3f;

        [field: Space] 
        [field: SerializeField] public float OnJumpGravity = 2.5f;
        [field: SerializeField] public float FallingGravity = 2.5f;
    }
}