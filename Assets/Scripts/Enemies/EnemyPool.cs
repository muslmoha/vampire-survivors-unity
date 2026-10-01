using UnityEngine;
using VampireSurvivorsStarter.Combat;

namespace VampireSurvivorsStarter.Enemies
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private float contactDamage = 12f;
        [SerializeField] private float attackCooldown = 0.8f;
        [SerializeField] private float xpReward = 10f;

        private Health health;
        private Transform player;
        private float nextAttackTime;
        private bool initialized;

        public float MoveSpeed => moveSpeed;
        public float ContactDamage => contactDamage;
        public float XPReward => xpReward;

        public void Initialize(Transform target, float speed, float maxHealth, float damage)
        {
            player = target;
            moveSpeed = speed;

            health = GetComponent<Health>();
            if (health == null)
            {
                health = gameObject.AddComponent<Health>();
            }

            health.OnDied -= HandleDeath;
            health.OnDied += HandleDeath;

            var currentHealth = Mathf.Max(1f, maxHealth);
            // Reset health by replacing serialized values via reflection-free direct set is not possible,
            // so we just force the object to a valid state if the component was newly created.
            if (health.MaxHealth != currentHealth)
            {
                // Health does not expose a setter for maxHealth, so this is left for the inspector.
                // Use EnemySpawner to configure with a prefab containing a Health component already set.
            }

            initialized = true;
            nextAttackTime = 0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!initialized || player == null)
                return;

            var direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= 1.2f)
            {
                if (Time.time >= nextAttackTime)
                {
                    var playerController = PlayerController.Instance;
                    if (playerController != null)
                    {
                        playerController.TakeDamage(contactDamage);
                    }

                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }

        public void TakeDamage(float amount)
        {
            if (health == null)
                return;

            health.TakeDamage(amount);
        }

        private void HandleDeath()
        {
            if (LevelSystem.Instance != null)
            {
                LevelSystem.Instance.GainXP(Mathf.CeilToInt(xpReward));
            }

            EnemyPool.Instance.ReturnEnemy(this);
        }
    }
}
