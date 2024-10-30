using Wonga.Data;

namespace Wonga.Test
{
    //attempts to test singleton, Move methods cannot be tested by this
    public class PlateauForTest : Plateau
    {
        public void UnInitialize()
        {
            InstanceForTest = null;
        }

        private static PlateauForTest _instanceForTest;
        public static PlateauForTest InstanceForTest
        {
            get
            {
                if (_instanceForTest == null)
                {
                    _instanceForTest = new PlateauForTest();
                }
                return _instanceForTest;
            }
            private set //just for tests, as long as IOC is not allowed for this program
            {
                _instanceForTest = value;
            }
        }
    }
}
