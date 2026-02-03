namespace GarageDoorController.GarageDoorStates
{
    internal class Stopped(GarageDoorController controller, Motor motor) : AbstractGarageDoorState(controller, motor)
    {
        public override void Enter()
        {
            Motor.Stop();
        }
    }
}