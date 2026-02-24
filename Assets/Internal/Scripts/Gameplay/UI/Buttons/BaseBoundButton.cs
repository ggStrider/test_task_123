using Internal.Scripts.Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Gameplay.UI.Buttons
{
    public abstract class BaseBoundButton : MonoBehaviour
    {
        [SerializeField] protected Button Button;

        private void Awake()
        {
            if (Button == null)
            {
                CustomDebugger.LogError(this, "Button is not attached!", gameObject);
                return;
            }

            Button.onClick.AddListener(ButtonClickAction);
        }

		protected abstract void ButtonClickAction();

#if UNITY_EDITOR
        private void Reset()
        {
            if (Button == null)
            {
                if (TryGetComponent<Button>(out var button))
                {
                    Button = button;
                }
            }
        }
#endif
    }
}