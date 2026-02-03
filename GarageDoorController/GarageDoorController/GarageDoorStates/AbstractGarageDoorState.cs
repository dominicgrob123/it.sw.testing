using GarageDoorController.Fsm;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GarageDoorController.GarageDoorStates
{
    public abstract class AbstractGarageDoorState : AbstractState
    {
        protected GarageDoorController Controller { get; }
        protected Motor Motor { get; }

        public AbstractGarageDoorState(GarageDoorController controller, Motor motor)
        {
            Controller = controller;
            Motor = motor;
        }

        public override void Enter()
        {
        }

        public override void Do()
        {
        }

        public override void Exit()
        {
        }

        internal virtual void KeyPressed()
        {
            NotifyUnknownEvent();
        }

        internal virtual void UpperEnd()
        {
            NotifyUnknownEvent();
        }

        internal virtual void LowerEnd()
        {
            NotifyUnknownEvent();
        }

        private void NotifyUnknownEvent([CallerMemberName] string name = "?")
        {
            Debug.WriteLine($"No implementation for {name}-Event in {GetType().Name}");
        }
    }
}
