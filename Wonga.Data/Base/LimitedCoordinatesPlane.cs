using System;

namespace Wonga.Data.Base
{
    public abstract class LimitedCoordinatesPlane<T> where T : IComparable<T>
    {
        public LimitedCoordinateAxis<T> XAxis { get; private set; }

        public LimitedCoordinateAxis<T> YAxis { get; private set; }

        protected LimitedCoordinatesPlane(T minX, T maxX, T minY, T maxY)
        {
            XAxis = new LimitedCoordinateAxis<T>(minX, maxX);
            YAxis = new LimitedCoordinateAxis<T>(minY, maxY);
        }

        /// <summary>
        /// Determines if the coordinate plane is valid
        /// </summary>
        /// <returns>True if coordinate plane is valid, else false</returns>
        public virtual Boolean IsValid()
        {
            return XAxis.IsValid() && YAxis.IsValid();
        }

        public virtual Boolean ContainsValues(T xvalue, T yvalue)
        {
            return XAxis.ContainsValue(xvalue) && YAxis.ContainsValue(yvalue);
        }

        public virtual Boolean ContainsXValue(T value)
        {
            return XAxis.ContainsValue(value);
        }

        public virtual Boolean ContainsYValue(T value)
        {
            return YAxis.ContainsValue(value);
        }
    }
}