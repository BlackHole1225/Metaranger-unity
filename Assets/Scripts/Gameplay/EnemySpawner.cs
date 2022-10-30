using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.AI
{
    public class EnemySpawner : MonoBehaviour
    {

        [Tooltip("The enemy that is spawned")]
        public GameObject enemy;

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
                Instantiate(enemy, transform.position, transform.rotation);
                if (spawnRate > 5.0f)
                {
                    spawnRate -= spawnAcceleration;
                }
                spawnTimer = Time.time + spawnRate;
            }
        }
    }
}