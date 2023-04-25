    public class StateMachineEnemy
    {
        public StateEnemy CurrentState { get; private set; }

        public void Initialize(StateEnemy startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(StateEnemy newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

