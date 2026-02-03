namespace GarageDoorController.GarageDoorStates
{
    internal class ClosingStopped(GarageDoorController controller, Motor motor) : Stopped(controller, motor)
    {
        internal override void KeyPressed()
        {
            Controller.SetNextState<Opening>();
        }
    }
}