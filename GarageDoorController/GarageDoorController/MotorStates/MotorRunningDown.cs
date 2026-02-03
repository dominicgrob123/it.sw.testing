namespace GarageDoorController.MotorStates
{
    internal class MotorRunningDown : AbstractMotorState
    {
        public override MotorDirection Direction => MotorDirection.Down;

        public override void Enter()
        {
        }
    }
}
