using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents.Engines
{
    public class InvalidEngineOperationException : Exception
    {
        public InvalidEngineOperationException()
            : base()
        {

        }

        public InvalidEngineOperationException(string message)
            : base(message)
        {

        }
    }
}
