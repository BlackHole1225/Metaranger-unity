using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.AI
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemies")]
        public GameObject Enemy_Hoverbot;
        public GameObject Enemy_Roller;
        public GameObject Enemy_Swarmer;
        public GameObject Enemy_SpecialOps;

        [Header("Spawn Variables")]
        [Tooltip("The rate at which enemies are spawned")]
        public float spawnRate = 45.0f;

        [Tooltip("The amount at which spawning is accelerated")]
        public float spawnAcceleration = 0.2f;

        private float spawnTimer;

        void Start()
        {

        }

        void Update()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (Time.time > spawnTimer)
            {

                int outcome = Random.Range(0, 12);
                GameObject enemyToSpawn;

                if (outcome < 4)
                {
                    enemyToSpawn = Enemy_Swarmer;
                }
                else if (outcome < 7)
                {
                    enemyToSpawn = Enemy_Hoverbot;
                }
                else if (outcome < 10)
                {
                    enemyToSpawn = Enemy_Roller;
                }
                else
                {
                    enemyToSpawn = Enemy_SpecialOps;
                }

                Instantiate(enemyToSpawn, transform.position, transform.rotation);

                if (spawnRate > 5.0f)
                {
                    spawnRate -= spawnAcceleration;
                }
                spawnTimer = Time.time + spawnRate;
            }
        }
    }
}