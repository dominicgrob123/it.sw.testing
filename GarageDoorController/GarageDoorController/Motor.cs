using GarageDoorController.Fsm;
using GarageDoorController.MotorStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDoorController
{
    public class Motor
    {
        private FsmRunner<AbstractMotorState> _fsm = new();

        public string MotorStateString => _fsm.CurrentState.DisplayName;

        public MotorDirection Direction => _fsm.CurrentState.Direction;

        public Motor()
        {
            _fsm.Init(new MotorStopped());
        }

        public void Up()
        {
            _fsm.SetNextState(new MotorRunningUp());
            _fsm.Run();
        }

        public void Down()
        {
            _fsm.SetNextState(new MotorRunningDown());
            _fsm.Run();
        }

        public void Stop()
        {
            _fsm.SetNextState(new MotorStopped());
            _fsm.Run();
        }
    }
}
