namespace GarageDoorController.Fsm
{
    public class FsmRunner<T> where T : notnull, IState
    {
        private T? _currentState;
        public T CurrentState
        {
            get
            {
                if (_currentState == null)
                {
                    throw new InvalidOperationException($"State machine is uninitialized");
                }
                return _currentState;
            }

            private set
            {
                if (value == null) 
                {
                    throw new InvalidDataException($"Current state must not be null!");    
                }
                _currentState = value;
            }
        }

        private T? NextState { get; set; }

        public void Init(T initial)
        {
            CurrentState = initial;
            NextState = initial;
            CurrentState.Enter();
        }

        public void SetNextState(T state)
        {
            NextState = state;
        }

        public void Run() {
            if ((CurrentState == null)
                || (NextState == null))

            {
                throw new InvalidOperationException($"State machine is uninitialized");
            }

            if (!NextState.Equals(CurrentState))
            {
                CurrentState.Exit();
                NextState.Enter();
                CurrentState = NextState;
            }

            CurrentState.Do();
        }
    }
}
