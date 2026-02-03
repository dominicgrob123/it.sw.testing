using GarageDoorController;
using GarageDoorController.MotorStates;

namespace GarageDoorSimulation
{
    internal class DoorSim
    {
        private Motor _motor;
        private MotorDirection _last_direction = MotorDirection.Stop;
        private float _timeout = 0;

        public float Position { get; set; }

        public int TimePerPercent { get; set; } = 100;

        public int InvocationPeriod { get; set; } = 200;

        public DoorSim(Motor motor)
        {
            _motor = motor;
        }

        public void Reset()
        {
            Position = 0f;
        }

        public void Move()
        {
            if (_last_direction != _motor.Direction)
            {
                _last_direction = _motor.Direction;
                _timeout = 0;
            }
            else if (_motor.Direction != MotorDirection.Stop)
            {
                _timeout += InvocationPeriod;
            }

            while (_timeout > TimePerPercent)
            {
                switch (_motor.Direction)
                {
                    case MotorDirection.Down:
                        Position += 0.01f;
                        break;

                    case MotorDirection.Up:
                        Position -= 0.01f;
                        break;

                    case MotorDirection.Stop:
                        break;
                }

                _timeout -= TimePerPercent;
            }
        }
    }
}
