
public class StateMachineEnemy
    {
        public IStateEnemy CurrentState { get; private set; }

        public void Initialize(IStateEnemy startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(IStateEnemy newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
}

