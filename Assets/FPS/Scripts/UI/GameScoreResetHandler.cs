using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class GameScoreResetHandler : MonoBehaviour
    {
        void Awake()
        {
            // Resets the game score once you reach the IntroMenu
            GameScore.playerScore = 0;
        }
    }
}