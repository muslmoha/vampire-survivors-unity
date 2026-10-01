using UnityEngine;
using VampireSurvivorsStarter.Enemies;

namespace VampireSurvivorsStarter
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Enemy enemyPrefab;

        private void Start()
        {
            if (playerTransform == null)
            {
                var player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    playerTransform = player.transform;
                }
            }

            if (playerTransform == null)
            {
                Debug.LogWarning("No player was found. Add a GameObject named Player with a PlayerController component.");
                return;
            }

            if (FindObjectOfType<EnemySpawner>() == null)
            {
                var spawner = new GameObject("EnemySpawner").AddComponent<EnemySpawner>();
                spawner.transform.SetParent(transform);
            }

            var enemySpawner = FindObjectOfType<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.SetPlayer(playerTransform);
            }
        }
    }
}
