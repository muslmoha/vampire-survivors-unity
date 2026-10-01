using UnityEngine;

namespace VampireSurvivorsStarter
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;

        private void Awake()
        {
            Instance = this;
            currentHealth = maxHealth;
        }

        public float CurrentHealth => currentHealth;

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            if (currentHealth <= 0f)
            {
                Debug.Log("Player defeated.");
            }
        }
    }
}
