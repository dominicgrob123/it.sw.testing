using GarageDoorController.Fsm;
using GarageDoorController.GarageDoorStates;

namespace GarageDoorController
{
    public class GarageDoorController
    {
        private readonly Motor _motor;

        public AbstractGarageDoorState CurrentState => FSM.CurrentState;
        public string DoorStateString => CurrentState.DisplayName;

        public FsmRunner<AbstractGarageDoorState> FSM { get; } = new();

        public GarageDoorController(Motor motor) {
            _motor = motor;
            FSM.Init(new Unknown(this, motor));
        }

        public void SetNextState<T>() where T : AbstractGarageDoorState
        {
            // This creates an instance of the state based on the type passed
            // along with the function in order to take the effort of constructing
            // new states from the lazy programmer. Typically the states could be
            // cached/reused, which is omitted here in order to keep the code simplified.
            // One could imagine, the code could be part of another library to be reused.
            var state = (AbstractGarageDoorState?)Activator.CreateInstance(typeof(T), new object[] { this, _motor });

            if (state == null)
            {
                throw new InvalidOperationException($"Could not create next state {typeof(T).Name}");
            }

            FSM.SetNextState(state);
        }

        public void Run()
        {
            FSM.Run();
        }

        #region Events
        public void KeyPressed()
        {
            CurrentState.KeyPressed();
        }

        public void LowerEnd()
        {
            CurrentState.LowerEnd();
        }

        public void UpperEnd()
        {
            CurrentState.UpperEnd();
        }
        #endregion
    }
}
