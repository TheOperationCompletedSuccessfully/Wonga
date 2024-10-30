using log4net;
using Wonga.Data.Base;

namespace Wonga.Data
{
    public sealed class Robot : RobotCoordinates, IRobotBehavoiur
    {
        #region Constructor

        public Robot(MarsCoordinates coordinates, int x, int y, Direction direction = Direction.N)
            : base(coordinates, x, y)
        {
            Direction = direction;
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof(Robot));

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X.Value, Y.Value, Direction);
        }
        
        #region IRobotBehavoiur

        public Direction Direction { get; private set; }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.N :
                    MoveNorth();
                    break;
                case Direction.S :
                    MoveSouth();
                    break;
                case Direction.E:
                    MoveEast();
                    break;
                case Direction.W :
                    MoveWest();
                    break;
            }
        }

        public void ExecuteCommand(MoveAction action)
        {
            switch (action)
            {
                case MoveAction.L:
                    TurnLeft();
                    break;
                case MoveAction.R:
                    TurnRight();
                    break;
                case MoveAction.M:
                    Move();
                    break;
            }
        }

        public void TurnLeft()
        {
            Direction = Direction == Direction.N ? Direction.W : --Direction;
        }

        public void TurnRight()
        {
            Direction = Direction == Direction.W ? Direction.N : ++Direction;
        }

        #endregion

        #region Helpers

        private void MoveNorth()
        {
            if (Plateau.Instance.IsPlaceOccupied(X, Y + 1))
            {
                Log.WarnFormat("Robot {0} cannot move north because place is occupied", ToString());
                return;
            }
            ++Y;
        }

        private void MoveSouth()
        {
            if (Plateau.Instance.IsPlaceOccupied(X, Y - 1))
            {
                Log.WarnFormat("Robot {0} cannot move south because place is occupied", ToString());
                return;
            }
            --Y;
        }

        private void MoveEast()
        {
            if (Plateau.Instance.IsPlaceOccupied(X + 1, Y))
            {
                Log.WarnFormat("Robot {0} cannot move east because place is occupied", ToString());
                return;
            }
            ++X;
        }

        private void MoveWest()
        {
            if (Plateau.Instance.IsPlaceOccupied(X - 1, Y))
            {
                Log.WarnFormat("Robot {0} cannot move west because place is occupied", ToString());
                return;
            }
            --X;
        }

        #endregion

    }

    public enum Direction
    {
        N,
        E,
        S,
        W
    }

    public enum MoveAction
    {
        L,
        R,
        M
    }
}
