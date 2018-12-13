using Joostit.UnitTestIntroCs.CarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.DriverMain
{
    public class RealRoad
    {
        public void Drive()
        {

            Console.WriteLine("Starting real road test");
            Car myCar = new Car("AB-CD-EF", 1300);
            myCar.Tires = TireTypes.Offroad;
            myCar.Start();

            myCar.SetSpeed(120);
            myCar.DriveMiles(16);
            myCar.Brake();

            myCar.Shutdown();

            Console.WriteLine("Total kilometers driven: " + myCar.DistanceKm);
        }
    }
}
