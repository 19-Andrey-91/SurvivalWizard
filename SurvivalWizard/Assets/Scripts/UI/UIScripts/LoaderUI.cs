
using SurvivalWizard.Base;
using SurvivalWizard.UI.StateUI;
using UnityEngine;

namespace SurvivalWizard.UI.UIScripts
{
    public class LoaderUI : MonoBehaviour
    {
        [Header("GameManager")]
        [SerializeField] private GameManager _prefabGameManager;
        [Header("Prefabs UI")]
        [SerializeField] private StartMenuUI _prefabStartUI;
        [SerializeField] private GameUI _prefabGameUI;
        [SerializeField] private PauseUI _prefabPauseUI;
        [SerializeField] private GameOverUI _prefabGameOverUI;
        [SerializeField] private SpellUpgradeUI _prefabSpellUpgradeUI;
        [SerializeField] private OptionsUI _prefabOptionsUI;
        public StateMachineUI StateMachineUI { get; private set; }
        public IStateUI StartMenuUIState { get; private set; }
        public IStateUI GameUIState { get; private set; }
        public IStateUI PauseUIState { get; private set; }
        public IStateUI GameOverUIState { get; private set; }
        public IStateUI UpgradeUIState { get; private set; }
        public IStateUI OptionsUIState { get; private set; }

        private void Awake()
        {
            StateMachineUI = new StateMachineUI();
            StartMenuUIState = new StartMenuUIState(this, Instantiate(_prefabStartUI, transform), _prefabGameManager);
            GameUIState = new GameUIState(this, Instantiate(_prefabGameUI, transform));
            PauseUIState = new PauseUIState(this, Instantiate(_prefabPauseUI, transform));
            GameOverUIState = new GameOverUIState(this, Instantiate(_prefabGameOverUI, transform));
            UpgradeUIState = new UpgradeUIState(this, Instantiate(_prefabSpellUpgradeUI, transform));
            OptionsUIState = new OptionsUIState(this, Instantiate(_prefabOptionsUI, transform));
        }
        private void Start()
        {
            StateMachineUI.Initialize(StartMenuUIState);
        }
    }
}
