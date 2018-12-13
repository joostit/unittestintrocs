using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents
{
    public class InvalidCarUsageException : Exception
    {
        public InvalidCarUsageException()
        {

        }

        public InvalidCarUsageException(string message)
          : base(message)
        {

        }
    }
}
