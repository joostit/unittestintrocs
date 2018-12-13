using Joostit.UnitTestIntroCs.CarComponents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joostit.UnitTestIntroCs.CarComponentsTests
{
    [TestFixture]
    public class CarTests
    {

        [TestCase]
        public void NewCar_WhenCreated_HasLicensePlace()
        {
            Car testCar = new Car("AB-123-C", 1250);

            string licensePlate = testCar.LicensePlate;
            testCar.Dispose();

            Assert.AreEqual("AB-123-C", licensePlate);
        }


        [TestCase]
        public void NewCar_WhenCreated_HasWeight()
        {
            Car testCar = new Car("AB-123-C", 1250);

            int weight = testCar.WeightKg;
            testCar.Dispose();

            Assert.AreEqual(1250, weight);
        }

        [TestCase]
        public void NewCar_WhenCreated_HasHasBoringWhiteColor()
        {
            Car testCar = new Car("AB-123-C", 1250);

            CarColors color = testCar.Color;
            testCar.Dispose();

            Assert.AreEqual(CarColors.BoringWhite, color);
        }


        [TestCase]
        public void NewCar_WhenSetDistanceKm_DistanceKmMatches()
        {
            Car testCar = new Car("AB-123-C", 1250);

            testCar.SetDistanceKm(1234);
            double readDistance = testCar.DistanceKm;
            testCar.Dispose();

            Assert.AreEqual(1234, readDistance);
        }


        [TestCase]
        public void NewCar_WhenResetDistance_DistanceKmIsZero()
        {
            Car testCar = new Car("AB-123-C", 1250);

            testCar.ResetDistance();
            double readDistance = testCar.DistanceKm;
            testCar.Dispose();

            Assert.AreEqual(0, readDistance);
        }

    }
}
