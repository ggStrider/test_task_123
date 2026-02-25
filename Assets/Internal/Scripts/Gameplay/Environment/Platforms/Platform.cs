using UnityEngine;

namespace Internal.Scripts.Gameplay.Environment.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private bool _isWide;
        public bool IsWide => _isWide;
    }
}