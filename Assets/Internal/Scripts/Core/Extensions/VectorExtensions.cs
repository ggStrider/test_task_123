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

        public static Vector3 WithZ(this Vector3 source, float z)
        {
            source.z = 0;
            return source;
        }
    }
}