namespace GarageDoorController.MotorStates
{
    internal class MotorRunningUp : AbstractMotorState
    {
        public override MotorDirection Direction => MotorDirection.Up;

        public override void Enter()
        {
        }
    }
}
