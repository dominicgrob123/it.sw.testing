using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDoorController.Fsm
{
    public abstract class AbstractState : IState
    {
        public string DisplayName => GetType().Name;

        public abstract void Enter();
        public abstract void Do();
        public abstract void Exit();
    }
}
