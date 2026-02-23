using Internal.Scripts.Core.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Internal.Scripts.Core.Inputs
{
    public class InputReader : IInitializable
    {
        public Vector2 Tilt => GetTilt();

        private Vector2 GetTilt()
        {
#if UNITY_EDITOR
            return GetDirectionFromKeyboard();
#else
            return GetDeviceTilt();
#endif
        }

        private Vector2 GetDirectionFromKeyboard()
        {
            var x = 0f;
            if (Keyboard.current.aKey.isPressed)
                x = -1;
            else if (Keyboard.current.dKey.isPressed)
                x = 1;

            return new(x, 0); 
        }

        private Vector2 GetDeviceTilt()
        {
            if (Accelerometer.current == null)
            {
                CustomDebugger.LogError(this, "No accelerometer!");
                return Vector2.zero;
            }

            return Accelerometer.current.acceleration.ReadValue();
        }

        public void Initialize()
        {
            if (Accelerometer.current != null)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }
        }
    }
}