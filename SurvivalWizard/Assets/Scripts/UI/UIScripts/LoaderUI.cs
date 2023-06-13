
using SurvivalWizard.Base;
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.StateUI;
using UnityEngine;
using Zenject;

namespace SurvivalWizard.UI.UIScripts
{
    public class LoaderUI : MonoBehaviour
    {
        [Header("Prefabs UI")]
        [SerializeField] private StartMenuUI _prefabStartUI;
        [SerializeField] private GameUI _prefabGameUI;
        [SerializeField] private PauseUI _prefabPauseUI;
        [SerializeField] private GameOverUI _prefabGameOverUI;
        [SerializeField] private SpellUpgradeUI _prefabSpellUpgradeUI;
        [SerializeField] private OptionsUI _prefabOptionsUI;
        [SerializeField] private AddingSpellUI _prefabAddingWeaponUI;
        [SerializeField] private ShopUI _prefabShopUI;

        private SoundManager _soundManager;
        private GameManager _gameManager;
        private Bank _bank;

        public StateMachineUI StateMachineUI { get; private set; }
        public IStateUI StartMenuUIState { get; private set; }
        public IStateUI GameUIState { get; private set; }
        public IStateUI GameOverUIState { get; private set; }
        public IStateUI UpgradeUIState { get; private set; }
        public IStateUI OptionsUIState { get; private set; }
        public IStateUI AddingWeaponUIState { get; private set; }
        public IStateUI ShopUIState { get; private set; }

        [Inject]
        private void Construct(SoundManager soundManager, GameManager gameManager, Bank bank)
        {
            _soundManager = soundManager;
            _gameManager = gameManager;
            _bank = bank;
            _gameManager.Pause(true);
        }

        private void Start()
        {
            StateMachineUI = new StateMachineUI();
            StartMenuUIState = new StartMenuUIState(this, Instantiate(_prefabStartUI, transform));
            GameUIState = new GameUIState(this, Instantiate(_prefabGameUI, transform), _gameManager, _bank);
            GameOverUIState = new GameOverUIState(this, Instantiate(_prefabGameOverUI, transform), _gameManager);
            UpgradeUIState = new SpellUpgradeUIState(this, Instantiate(_prefabSpellUpgradeUI, transform), _gameManager);
            OptionsUIState = new OptionsUIState(this, Instantiate(_prefabOptionsUI, transform), _soundManager, _gameManager);
            AddingWeaponUIState = new AddingSpellUIState(this, Instantiate(_prefabAddingWeaponUI, transform), _gameManager);
            ShopUIState = new ShopUIState(this, Instantiate(_prefabShopUI, transform), _gameManager, _bank);

            StateMachineUI.Initialize(StartMenuUIState);
        }
    }
}
