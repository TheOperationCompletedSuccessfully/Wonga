namespace Wonga.Data.Base
{
    public class MarsCoordinateValue : LimitedCoordinateValue<int>
    {
        #region Constructor

        public MarsCoordinateValue(LimitedCoordinateAxis<int> axis, int value) : base(axis, value)
        {

        }

        #endregion

        #region Operators

        public static MarsCoordinateValue operator ++(MarsCoordinateValue val)
        {
            ++val.Value;
            return val;
        }

        public static MarsCoordinateValue operator --(MarsCoordinateValue val)
        {
            --val.Value;
            return val;
        }

        public static MarsCoordinateValue operator +(MarsCoordinateValue val, int intVal)
        {
            return new MarsCoordinateValue(val.Axis, val.Value + intVal);
        }

        public static MarsCoordinateValue operator -(MarsCoordinateValue val, int intVal)
        {
            return new MarsCoordinateValue(val.Axis, val.Value - intVal);
        }

        #endregion
    }
}
