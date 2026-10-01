using UnityEngine;

namespace VampireSurvivorsStarter.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform player;
        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private int maxEnemies = 500;
        [SerializeField] private float spawnRadius = 18f;
        [SerializeField] private float baseEnemySpeed = 1.8f;
        [SerializeField] private float baseEnemyHealth = 35f;
        [SerializeField] private float baseEnemyDamage = 10f;

        private float spawnTimer;

        private void Start()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>()?.transform;
            }

            spawnTimer = 0f;

            if (EnemyPool.Instance == null)
            {
                var pool = new GameObject("EnemyPool").AddComponent<EnemyPool>();
                pool.transform.SetParent(transform);
            }
        }

        private void Update()
        {
            if (player == null)
                return;

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f && EnemyPool.Instance.ActiveCount < maxEnemies)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnEnemy()
        {
            if (EnemyPool.Instance == null)
                return;

            var offset = Random.insideUnitCircle.normalized * spawnRadius;
            var spawnPosition = player.position + new Vector3(offset.x, 0f, offset.y);
            var speed = baseEnemySpeed + Random.Range(-0.15f, 0.35f);
            var health = baseEnemyHealth + Random.Range(0f, 20f);
            var damage = baseEnemyDamage + Random.Range(0f, 4f);

            EnemyPool.Instance.Get(spawnPosition, player, speed, health, damage);
        }
    }
}
