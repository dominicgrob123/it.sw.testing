namespace GarageDoorController.GarageDoorStates
{
    internal class OpeningStopped(GarageDoorController controller, Motor motor) : Stopped(controller, motor)
    {
        internal override void KeyPressed()
        {
            Controller.SetNextState<Closing> ();
        }
    }
}