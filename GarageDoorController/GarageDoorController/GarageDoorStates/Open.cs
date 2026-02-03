namespace GarageDoorController.GarageDoorStates
{
    internal class Open(GarageDoorController controller, Motor motor) : Stopped(controller, motor)
    {
        internal override void KeyPressed()
        {
            Controller.SetNextState<Closing>();
        }
    }
}