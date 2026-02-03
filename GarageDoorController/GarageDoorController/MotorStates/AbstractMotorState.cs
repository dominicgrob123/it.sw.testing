using GarageDoorController.Fsm;

namespace GarageDoorController.MotorStates
{
    public enum MotorDirection
    {
        Stop,
        Up,
        Down
    }

    internal abstract class AbstractMotorState : AbstractState
    {
        public abstract MotorDirection Direction { get; }

        public override void Enter()
        {
        }

        public override void Do()
        {
        }

        public override void Exit()
        {
        }
    }
}
