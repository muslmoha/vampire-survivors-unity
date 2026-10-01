using UnityEngine;

namespace VampireSurvivorsStarter.Combat
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsDead => currentHealth <= 0f;

        public event System.Action<float> OnDamageTaken;
        public event System.Action OnDied;

        private void Reset()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0f || IsDead)
                return;

            currentHealth = Mathf.Max(0f, currentHealth - amount);
            OnDamageTaken?.Invoke(amount);

            if (currentHealth <= 0f)
            {
                OnDied?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (amount <= 0f || IsDead)
                return;

            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        }
    }
}
