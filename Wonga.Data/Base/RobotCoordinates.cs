using System;
using Wonga.Data.Exceptions;

namespace Wonga.Data.Base
{
    public abstract class RobotCoordinates
    {
        #region Constructor

        protected RobotCoordinates(MarsCoordinates coordinates, int x, int y)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates", "Coordinates are null");
            }
            _surface = coordinates;
            X = new MarsCoordinateValue(coordinates.XAxis, x);
            Y = new MarsCoordinateValue(coordinates.YAxis, y);
        }

        #endregion

        private readonly MarsCoordinates _surface;

        private MarsCoordinateValue _x;
        public MarsCoordinateValue X
        {
            get
            {
                return _x;
            }
            protected set
            {
                if (!_surface.ContainsXValue(value.Value))
                {
                    throw new CoordinatesOverflowException();
                }
                _x = value;
            }
        }

        private MarsCoordinateValue _y;
        public MarsCoordinateValue Y
        {
            get
            {
                return _y;
            }
            protected set
            {
                if (!_surface.ContainsYValue(value.Value))
                {
                    throw new CoordinatesOverflowException();
                }
                _y = value; 
            }
        }

        public bool CheckCoordinates()
        {
            if (!_surface.ContainsValues(_x.Value, _y.Value))
            {
                return false;
            }

            return true;
        }

       
    }
}
