namespace Wonga.Data.Base
{
    //plateau always starts with 0 0
    public class MarsCoordinates : LimitedCoordinatesPlane<int>
    {
        public MarsCoordinates(int maxX, int maxY) : base(0, maxX, 0, maxY)
        {

        }
        
    }
}
