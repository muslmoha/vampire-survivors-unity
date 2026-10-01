using UnityEngine;

namespace VampireSurvivorsStarter
{
    public class LevelSystem : MonoBehaviour
    {
        public static LevelSystem Instance { get; private set; }

        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentXP = 0;
        [SerializeField] private int xpToNextLevel = 100;

        public int CurrentLevel => currentLevel;
        public int CurrentXP => currentXP;
        public int XPToNextLevel => xpToNextLevel;

        private void Awake()
        {
            Instance = this;
        }

        public void GainXP(int amount)
        {
            if (amount <= 0)
                return;

            currentXP += amount;
            while (currentXP >= xpToNextLevel)
            {
                currentXP -= xpToNextLevel;
                currentLevel++;
                xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.35f);
                Debug.Log($"Player leveled up! Level {currentLevel}");
            }
        }
    }
}
