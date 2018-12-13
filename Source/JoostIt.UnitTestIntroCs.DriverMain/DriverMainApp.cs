using Joostit.UnitTestIntroCs.CarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.DriverMain
{
    class DriverMainApp
    {
        static void Main(string[] args)
        {
            RealRoad road = new RealRoad();
            road.Drive();

            CarTestTrack track = new CarTestTrack();
            track.Drive();

            Console.WriteLine("Press the any key to exit...");
            Console.ReadKey();
        }
    }
}
