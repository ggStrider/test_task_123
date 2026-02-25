using Internal.Scripts.Core.Data;
using TMPro;
using UnityEngine;

namespace Internal.Scripts.Gameplay.UI.Views.Additional
{
    // Instatiated NOT WITH DiContainer, so if you want to
    // inject something, change the way it instantiating
    public class RecordUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordLabel;

        public void Initialize(int index, GameHighScoreRecord recordToShowHere)
        {
            _recordLabel.text = $"{(index + 1)}. {recordToShowHere.GetFormatted()}";
        }
    }
}