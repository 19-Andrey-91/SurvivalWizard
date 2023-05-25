
using System;

namespace SurvivalWizard.PlayerScripts
{
    public class Wallet
    {
        public event Action<int> OnChangeCoinsEvent;

        public int CurrentCoins { get; private set; }

        public Wallet(int currentCoins)
        {
            CurrentCoins = currentCoins;
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
    }
}
