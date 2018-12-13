using Joostit.UnitTestIntroCs.CarComponents.Engines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponents
{
    public class Car : IDisposable
    {
        private const double MilesPerKilometer = 0.621371192;
        private string distanceFileNameBase = "distanceDriven_{0}.txt";

        private double _DistanceKm = 0;
        private CarEngine _Engine = null;

        public double SpeedKmh { get; private set; }

        public int WeightKg { get; private set; } = 1300;

        public string LicensePlate { get; private set; } = null;

        public CarColors Color { get; private set; } = CarColors.BoringWhite;

        public TireTypes Tires { get; set; } = TireTypes.None;

        public CarEngine Engine
        {
            get
            {
                return _Engine;
            }
            private set
            {
                if (_Engine != null)
                {
                    if (_Engine.State != EngineStates.Stopped && _Engine.State != EngineStates.Error)
                    {
                        throw new InvalidCarUsageException("Cannot replace the engine when it's not stopped");
                    }
                }
                _Engine = value;
            }
        }

        public double DistanceKm
        {
            get
            {
                return _DistanceKm;
            }
            set
            {
                if (!Double.Equals(DistanceKm, value))
                {
                    _DistanceKm = value;
                    StoreDistanceInfo();
                }
            }
        }

        public double DistanceMiles
        {
            get
            {
                return DistanceKm / MilesPerKilometer;
            }
            private set
            {
                DistanceKm = value * MilesPerKilometer;
            }
        }

        public Car(string licensePlate, int weightKg)
        {
            Engine = new DieselEngine(PistonConfigurations.Inline4, 1.6);
            this.LicensePlate = licensePlate != null ? licensePlate : String.Empty;
            this.WeightKg = weightKg;

            InitializeDistanceInfo();
        }


        public Car() : 
            this(null, 0)
        {
        }

        private string getDistanceFileName()
        {
            string license = !String.IsNullOrEmpty(LicensePlate) ? LicensePlate : "-";
            return String.Format(distanceFileNameBase, license);
        }

        private void InitializeDistanceInfo()
        {
            try
            {
                using (TextReader reader = new StreamReader(getDistanceFileName()))
                {
                    string distance = reader.ReadLine();
                    double readKm = Convert.ToDouble(distance);
                    _DistanceKm = readKm;
                }
            }
            catch (FileNotFoundException)
            {
                InitNewDistanceFile();
            }
            catch (FormatException)
            {
                InitNewDistanceFile();
            }
        }

        private void InitNewDistanceFile()
        {
            using(TextWriter tw = new StreamWriter(getDistanceFileName(), false))
            {
                tw.WriteLine(0);
            }
        }

        private void StoreDistanceInfo()
        {
            using (TextWriter tw = new StreamWriter(getDistanceFileName(), false))
            {
                tw.WriteLine(DistanceKm);
            }
        }

        public void Repaint(CarColors newColor)
        {
            // First save and take off the license plate before starting the paint job
            string licensePlateStorage = LicensePlate;
            LicensePlate = null;

            // Do the paint job
            Color = newColor;

            // After painting is complete, put the license plate back on 
            licensePlateStorage = LicensePlate;
        }


        public void Start()
        {
            bool engineStarted = false;

            try
            {
                engineStarted = Engine.Start();
            }
            catch (EngineFailureException exc)
            {
                throw new CarBrokenException("The car is broken: Cannot start the engine.", exc);
            }

            if (!engineStarted)
            {
                throw new CarBrokenException("The engine couldn't start");
            }
        }


        public void SetSpeed(double speedKmh)
        {
            if(Tires == TireTypes.None)
            {
                throw new InvalidCarUsageException("The car speed cannot be set when there are no tires");
            }

            double throttleToSet = (speedKmh * WeightKg) / (245000 * Math.Log10(Engine.MaxPowerKw / 10));
            Engine.Throttle = (int) Math.Round(throttleToSet * 100);
            SpeedKmh = speedKmh;
        }


        public void DriveMiles(double milesToDrive)
        {
            if (SpeedKmh > 0)
            {
                DistanceMiles += milesToDrive;
            }
            else
            {
                throw new InvalidCarUsageException("Cannot drive the specified distance when the speed is zero");
            }
        }


        public void DriveKilometers(double kmToDrive)
        {
            if (SpeedKmh > 0)
            {
                DistanceKm += kmToDrive;
            }
            else
            {
                throw new InvalidCarUsageException("Cannot drive the specified distance when the speed is zero");
            }
        }


        public void ResetDistance()
        {
            DistanceKm = 0;
        }

        public void SetDistanceKm(double value)
        {
            DistanceKm = value;
        }

        public void Brake()
        {
            Engine.Throttle = 0;
        }

        
        public void Shutdown()
        {
            Engine.Stop();
        }

        /// <summary>
        /// Calling Dispose is mandatory after using a Car
        /// </summary>
        public void Dispose()
        {
            // Very important stuff is disposed here
            // It should always be called after using a car
            GC.SuppressFinalize(this);
        }
    }
}
