using System;
using Wonga.Data.Exceptions;

namespace Wonga.Data.Base
{
    public class LimitedCoordinateValue<T>  where T : IComparable<T>
    {
        #region Constructor

        public LimitedCoordinateValue(LimitedCoordinateAxis<T> axis, T value)
        {
            Axis = axis;
            Value = value;
        }

        #endregion

        public LimitedCoordinateAxis<T> Axis { get; private set; }

        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            protected set
            {
                if (!Axis.ContainsValue(_value))
                {
                    throw new CoordinatesOverflowException();
                }
                _value = value;
            }
        }
    }
}
