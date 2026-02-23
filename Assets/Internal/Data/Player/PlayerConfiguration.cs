using UnityEngine;

namespace Internal.Data.Player
{
    [CreateAssetMenu(fileName = "New Player Configuration", menuName = "Game Jumper/Player/Configuration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [field: SerializeField] public float JumpForce { get; private set; } = 10f;
    }
}