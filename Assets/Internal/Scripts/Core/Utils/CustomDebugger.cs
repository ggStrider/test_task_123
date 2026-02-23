using UnityEngine;

namespace Internal.Scripts.Core.Utils
{
    public static class CustomDebugger
    {
        /// <summary>
        /// Logs an error. Body compiles only in editor and development build
        /// </summary>
        /// <param name="sender">Who sending this message</param>
        /// <param name="message">message to log</param>
        public static void LogError(object sender, string message, GameObject senderGameObject = null)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var formattedMessage = GetFormattedMessage(sender, message, senderGameObject);
            Debug.LogError(formattedMessage);
#endif
        }

        public static string GetFormattedMessage(object sender, string message, GameObject senderGameObject = null)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var formattedMessage = $"[{sender.GetType().Name}] {message}";

            if (senderGameObject != null)
                formattedMessage += $"\nGameObject: {senderGameObject.name}";

            return formattedMessage;
#endif
            
#pragma warning disable CS0162 // Unreachable code detected
            // prevents error in build
            return string.Empty;
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}