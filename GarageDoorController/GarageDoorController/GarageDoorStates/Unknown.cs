namespace GarageDoorController.GarageDoorStates
{
    internal class Unknown(GarageDoorController controller, Motor motor) : AbstractGarageDoorState(controller, motor)
    {
        public override void Enter()
        {
            Motor.Stop();
        }

        internal override void KeyPressed()
        {
            Controller.SetNextState<Closing>();
        }
    }
}
