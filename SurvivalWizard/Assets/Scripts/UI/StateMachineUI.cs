

namespace SurvivalWizard.UI
{
    public class StateMachineUI
    {
        public IStateUI CurrentState { get; private set; }

        public void Initialize(IStateUI startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(IStateUI newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
