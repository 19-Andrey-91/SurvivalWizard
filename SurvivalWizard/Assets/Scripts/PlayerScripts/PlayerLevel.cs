
using System;

namespace SurvivalWizard.PlayerScripts
{
    public class PlayerLevel
    {
        public event Action<int> OnIncreasedLevelEvent;
        public event Action<int> OnExperienceAddedEvent;

        private int _levelBoostFactor;

        private int _level = 1;
        private int _experience = 0;

        public int ToLevelUP { get => _level * _levelBoostFactor; }

        public PlayerLevel(int levelBoostFactor)
        {
            _levelBoostFactor = levelBoostFactor;
        }

        public void AddExperience(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            _experience += amount;
            CheckLevelUp();
            OnExperienceAddedEvent?.Invoke(_experience);
        }

        private void CheckLevelUp()
        {
            if (_experience >= ToLevelUP)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _experience -= ToLevelUP;
            _level += 1;
            OnIncreasedLevelEvent?.Invoke(_level);
        }
    }
}
