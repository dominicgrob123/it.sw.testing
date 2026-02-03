namespace GarageDoorController.GarageDoorStates
{
    internal class Opening(GarageDoorController controller, Motor motor) : AbstractGarageDoorState(controller, motor)
    {
        public override void Enter()
        {
            Motor.Up();
        }

        internal override void KeyPressed()
        {
            Controller.SetNextState<OpeningStopped>();
        }

        internal override void UpperEnd()
        {
            Controller.SetNextState<Open>();
        }
    }
}