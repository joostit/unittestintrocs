using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents
{
    public class CarBrokenException : Exception
    {
        public CarBrokenException()
            : base()
        {

        }

        public CarBrokenException(string message)
            : base(message)
        {

        }

        public CarBrokenException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

    }
}
