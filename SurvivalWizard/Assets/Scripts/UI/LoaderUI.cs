
using SurvivalWizard.Base;
using SurvivalWizard.UI.StateUI;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI
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
        public StateMachineUI StateMachineUI { get; private set; }
        public IStateUI StartMenuUIState { get; private set; }
        public IStateUI GameUIState { get; private set; }
        public IStateUI PauseUIState { get; private set; }
        public IStateUI GameOverUIState { get; private set; }
        public IStateUI UpgradeState { get; private set; }

        private void Awake()
        {
            StateMachineUI = new StateMachineUI();
            StartMenuUIState = new StartMenuUIState(this, Instantiate(_prefabStartUI), _prefabGameManager);
            GameUIState = new GameUIState(this, Instantiate(_prefabGameUI));
            PauseUIState = new PauseUIState(this, Instantiate(_prefabPauseUI));
            GameOverUIState = new GameOverUIState(this, Instantiate(_prefabGameOverUI));
            UpgradeState = new UpgradeUIState(this, Instantiate(_prefabSpellUpgradeUI));
        }
        private void Start()
        {
            StateMachineUI.Initialize(StartMenuUIState);
        }
    }
}
