using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class PostGameScore : MonoBehaviour
    {
        [Tooltip("Test for the Player's Score")]
        public TextMeshProUGUI GameScoreLabel;

        void Awake()
        {
            GameScoreLabel.text = $"You scored {GameScore.playerScore} points";
        }
    }
}