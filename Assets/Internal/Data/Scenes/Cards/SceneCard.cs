using UnityEngine;

namespace Internal.Data.Scenes.Cards
{
    [CreateAssetMenu(fileName = "New Scene Card", menuName = "Game Jumper/Scenes/Card")]
    public class SceneCard : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}