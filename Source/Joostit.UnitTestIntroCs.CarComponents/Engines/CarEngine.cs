using Joostit.UnitTestIntroCs.CarComponents.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents.Engines
{
    public abstract class CarEngine
    {
        private int _throttle = 0;

        public EngineStates State { get; protected set; } = EngineStates.Stopped;
        public double MaxPowerKw { get; protected set; }

        public int Throttle
        {
            get
            {
                return _throttle;
            }
            set
            {
                validateNewThrottleValue(value);
                _throttle = value;
                ApplyNewThrottleValue();
            }
        }


        public CarEngine()
        {

        }

        private void validateNewThrottleValue(int value)
        {
            switch (State)
            {
                case EngineStates.Stopped:
                case EngineStates.Starting:
                case EngineStates.Error:
                    if(value != 0)
                    {
                        throw new InvalidEngineOperationException("Cannot set another throttle value than 0 when the engine isn't running");
                    }
                    break;

                case EngineStates.Running:
                    if((value < 0 ) || (value > 100))
                    {
                        throw new InvalidEngineOperationException("The throttle value should be in the range from 0 to 100 percent.");
                    }
                    break;

                default:
                    throw new NotSupportedException("Bug: Unsupported engine state");
            }            
        }


        public abstract bool Start();

        public abstract void Stop();

        protected abstract void ApplyNewThrottleValue();

    }
}
