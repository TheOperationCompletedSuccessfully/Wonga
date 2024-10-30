using System;

namespace Wonga.Data.Base
{
    public class LimitedCoordinateAxis<T> where T : IComparable<T>
    {
        public T MinValue { get; protected set; }

        public T MaxValue { get; protected set; }

        public LimitedCoordinateAxis(T minValue, T maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public virtual Boolean IsValid()
        {
            return MinValue.CompareTo(MaxValue) <= 0;
        }

        public virtual Boolean ContainsValue(T value)
        {
            return (MinValue.CompareTo(value) <= 0) && (value.CompareTo(MaxValue) <= 0);
        }
    }
}
