using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.AI
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemy;

        public float spawnRate = 5.0f;
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
                spawnTimer = Time.time + spawnRate;
            }
        }
    }
}