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

        async void OnKillEnemy(EnemyKillEvent evt)
        {
            string enemyName = evt.Enemy.name;
            int scoreIncrement = 0;

            if (enemyName.Contains("Enemy_HoverBot") || enemyName.Contains("Enemy_Swarmer"))
            {
                scoreIncrement = 10;
            }
            else if (enemyName.Contains("Enemy_Roller"))
            {
                scoreIncrement = 15;
            }
            else if (enemyName.Contains("Enemy_SpecialOps"))
            {
                scoreIncrement = 20;
            }

            playerScore += scoreIncrement;
            GameScoreLabel.text = $"SCORE: {playerScore}";
        }

        void Destroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnKillEnemy);
        }


    }
}