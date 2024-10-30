using Wonga.Data.Base;

namespace Wonga.Data
{
    public static class Extensions
    {
        public static bool IsPlaceOccupied(this Robot robot, MarsCoordinateValue x, MarsCoordinateValue y)
        {
            return x.Value == robot.X.Value && y.Value == robot.Y.Value;
        }
    }
}
