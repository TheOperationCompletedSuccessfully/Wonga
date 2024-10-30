using System;
using System.Collections.Generic;
using System.Linq;
using Wonga.Data.Base;
using Wonga.Data.Exceptions;

namespace Wonga.Data
{
    public class Plateau : ISurfaceInitializable
    {
        #region Singleton implementation

        private static Plateau _instance;

        protected Plateau()
        {
            _robots = new List<Robot>();
        }

        public static Plateau Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Plateau();
                }
                return _instance;
            }
        }

        #endregion

        #region Initializable

        private bool _isInitialized;
        public void Initialize(int x, int y)
        {
            if (_isInitialized) return;

            if (x < 0) throw new ArgumentException("bad value for x size of plateau", "x");
            if (y < 0) throw new ArgumentException("bad value for y size of plateau", "y");

            Coordinates = new MarsCoordinates(x, y);
            
            _isInitialized = true;
        }

        #endregion


        public MarsCoordinates Coordinates { get; private set; }
        private readonly IList<Robot> _robots;

        public void AddRobot(Robot newRobot)
        {
            if (!_isInitialized) throw new PlateauNotInitializedException("Plateau coordinates weren't initialized");

            if (IsPlaceOccupied(newRobot.X, newRobot.Y))
                throw new RobotPlacementException("Robot is set to not vacant place");

            _robots.Add(newRobot);
        }

        public bool IsPlaceOccupied(MarsCoordinateValue x, MarsCoordinateValue y)
        {
            return _robots.Where(r => r.CheckCoordinates()).Any(r => r.IsPlaceOccupied(x, y));
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _robots.Where(r=>r.CheckCoordinates()));
        }
    }
}
