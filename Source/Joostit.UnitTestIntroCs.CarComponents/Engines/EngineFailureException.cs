using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents.Engines
{
    public class EngineFailureException : Exception
    {
        public EngineFailureException() :
            base()
        {

        }

        public EngineFailureException(string message)
            : base(message)
        {

        }

        public EngineFailureException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
