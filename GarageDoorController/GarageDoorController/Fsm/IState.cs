namespace GarageDoorController.Fsm
{
    public interface IState
    {
        string DisplayName { get; }

        void Enter();
        void Do();
        void Exit();
    }
}