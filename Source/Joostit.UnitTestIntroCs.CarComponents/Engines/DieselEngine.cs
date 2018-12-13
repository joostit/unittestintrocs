using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents.Engines
{
    public class DieselEngine : CarEngine
    {

        private const int GlowTimePerDisplacementLiterMs = 1000;
        private const double RoomTemperatureC = 10;
        private const double PreglowTemperatureC = 30;
        private const double MinimumEngineStartTemperature = 25;

        public PistonConfigurations PistonConfiguration { get; private set; }
        public double Displacement { get; private set; }
        public double InternalTemperature { get; private set; } = RoomTemperatureC;


        public DieselEngine(PistonConfigurations pistonConfig, double displacement)
        {
            this.PistonConfiguration = pistonConfig;
            this.Displacement = displacement;

            DetermineMaxPower();
        }


        public override bool Start()
        {
            State = EngineStates.Starting;

            DoPreGlow();
            DoCrankTillStart();

            return (State == EngineStates.Running);
        }

        
        private void DoPreGlow()
        {
            Console.WriteLine("Engine pre-glow started...");
            int glowTime = (int) (Displacement * GlowTimePerDisplacementLiterMs);
            Thread.Sleep(glowTime);
            InternalTemperature = PreglowTemperatureC;
            Console.WriteLine("Engine pre-glow finished...");
        }


        private void DoCrankTillStart()
        {
            if (InternalTemperature < MinimumEngineStartTemperature)
            {
                throw new EngineFailureException("Cannot start the engine when the internal temperature is too low");
            }

            Console.WriteLine("Start engine cranking...");
            int crankTime = (int)(Displacement * new Random().NextDouble() * 3000);
            Thread.Sleep(crankTime);
            State = EngineStates.Running;
            Console.WriteLine("Engine cranking done...");

            ApplyNewThrottleValue();
        }


        public override void Stop()
        {
            Console.WriteLine("Engine is stopping");
            Thread.Sleep(500);
            Throttle = 0;
            State = EngineStates.Stopped;
            InternalTemperature = RoomTemperatureC;
            Console.WriteLine("Engine is stopped");
        }

        protected override void ApplyNewThrottleValue()
        {
            InternalTemperature = PreglowTemperatureC + Throttle;
        }

        protected void DetermineMaxPower()
        {
            double multiplier = 0;
            switch (PistonConfiguration)
            {
                case PistonConfigurations.Hamsterball1:
                    multiplier = 30;
                    break;
                case PistonConfigurations.Boxer2:
                    multiplier = 50;
                    break;
                case PistonConfigurations.Boxer4:
                    multiplier = 70;
                    break;
                case PistonConfigurations.CoffeeGrinder3:
                    multiplier = 50;
                    break;
                case PistonConfigurations.Inline4:
                    multiplier = 75;
                    break;
                case PistonConfigurations.Inline5:
                    multiplier = 90;
                    break;
                case PistonConfigurations.Inline6:
                    multiplier = 95;
                    break;
                case PistonConfigurations.V6:
                    multiplier = 96;
                    break;
                case PistonConfigurations.V8:
                    multiplier = 97;
                    break;
                case PistonConfigurations.W12:
                    multiplier = 98;
                    break;
                default:
                    throw new NotSupportedException("Unkown engine piston configuration");
            }

            MaxPowerKw = Displacement * multiplier;

        }
    }
}
