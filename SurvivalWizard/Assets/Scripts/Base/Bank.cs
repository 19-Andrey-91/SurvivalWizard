
using System;

namespace SurvivalWizard.Base
{
    public class Bank
    {
        public event Action<int> OnChangeCoinsEvent;

        public readonly IStorageService StorageService;

        private const string _nameLoadFile = "Coins";

        public int CurrentCoins { get; private set; }

        public Bank()
        {
            StorageService = new JsonToFileStorageService();
            StorageService.Load<int>(_nameLoadFile, LoadCoins);
        }

        private void LoadCoins(int coins)
        {
            CurrentCoins = coins;
        }

        public void AddCoins(int coins)
        {
            if (coins < 0)
            {
                throw new ArgumentOutOfRangeException("Argument AddCoins less 0");
            }
            CurrentCoins += coins;
            OnChangeCoinsEvent?.Invoke(CurrentCoins);
        }

        public bool TryPayCoins(int coins)
        {
            if (coins < 0)
            {
                throw new ArgumentOutOfRangeException("Argument TryPayCoins less 0");
            }
            if (CurrentCoins - coins < 0)
            {
                return false;
            }
            else
            {
                CurrentCoins -= coins;
                OnChangeCoinsEvent?.Invoke(CurrentCoins);
                return true;
            }
        }

        public void SaveCoins()
        {
            StorageService.Save(_nameLoadFile, CurrentCoins);
        }
    }
}
