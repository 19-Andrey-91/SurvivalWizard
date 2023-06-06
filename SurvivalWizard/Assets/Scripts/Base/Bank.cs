
using System;

namespace SurvivalWizard.Base
{
    public class Bank : Singleton<Bank>
    {
        public event Action<int> OnChangeCoinsEvent;

        private IStorageService _storageService;

        public int CurrentCoins { get; private set; }
        public IStorageService StorageService { get => _storageService; }

        protected override void Awake()
        {
            base.Awake();
            _storageService = new JsonToFileStorageService();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
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
