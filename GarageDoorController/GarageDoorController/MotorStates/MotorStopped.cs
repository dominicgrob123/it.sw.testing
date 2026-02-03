namespace GarageDoorController.MotorStates
{
    internal class MotorStopped : AbstractMotorState
    {
        public override MotorDirection Direction => MotorDirection.Stop;

        public override void Enter()
        {
        }
    }
}
