namespace GarageDoorController.GarageDoorStates
{
    internal class Closing(GarageDoorController controller, Motor motor) : AbstractGarageDoorState(controller, motor)
    {
        public override void Enter()
        {
            Motor.Down();
        }

        internal override void KeyPressed()
        {
            Controller.SetNextState<ClosingStopped>();
        }

        internal override void LowerEnd()
        {
            Controller.SetNextState<Closed>();
            Motor.Stop();
        }
    }
}
