using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class GameScore : MonoBehaviour
    {
        [Tooltip("Text for the Player's Score")]
        public TextMeshProUGUI GameScoreLabel;

        public static int playerScore = 0;

        ObjectiveKillEnemies _ObjectiveKillEnemies;

        void Awake()
        {
            EventManager.AddListener<EnemyKillEvent>(OnKillEnemy);
            GameScoreLabel.text = $"SCORE: {playerScore}";
        }

        void OnKillEnemy(EnemyKillEvent evt)
        {
            playerScore += 10;
            GameScoreLabel.text = $"SCORE: {playerScore}";
        }

        void Destroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnKillEnemy);
        }


    }
}