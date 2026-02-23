using UnityEngine;

namespace Internal.Scripts.Core.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithY(this Vector3 source, float newY)
        {
            source.y = newY;
            return source;
        }
    }
}