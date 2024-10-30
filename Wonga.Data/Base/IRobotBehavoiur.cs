namespace Wonga.Data.Base
{
    public interface IRobotBehavoiur
    {
        Direction Direction { get; }
        void Move();
        void ExecuteCommand(MoveAction action);
        void TurnLeft();
        void TurnRight();
    }
}