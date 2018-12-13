using Joostit.UnitTestIntroCs.CarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.DriverMain
{
    public class CarTestTrack
    {

        public void Drive()
        {
            Console.WriteLine("Starting Test Track stuff");

            Car testCar = new Car();
            testCar.Repaint(CarColors.ForestGreen);
            testCar.Start();

            Console.WriteLine("Racing the test track...");
            //
            // Lots of test track stuff happens here
            // ..
            // ..

            testCar.Shutdown();

            Console.WriteLine("Test track stuff finished");
        }

    }
}
