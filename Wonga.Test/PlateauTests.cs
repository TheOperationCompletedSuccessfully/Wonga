using System;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wonga.Data;
using Wonga.Data.Base;
using Wonga.Data.Exceptions;

namespace Wonga.Test
{
    [TestClass]
    public class PlateauTests
    {
        [ClassInitialize]
        public static void PlateauTestsInitialize(TestContext context)
        {
            XmlConfigurator.Configure();
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            PlateauForTest.InstanceForTest.UnInitialize();
        }
        
        [TestMethod]
        public void PlateauTests_InitializeTwice_OldValuesAreKept()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10,20);

            Assert.IsNotNull(plateau);
            Assert.AreEqual(0, plateau.Coordinates.XAxis.MinValue);
            Assert.AreEqual(0, plateau.Coordinates.YAxis.MinValue);
            Assert.AreEqual(10, plateau.Coordinates.XAxis.MaxValue);
            Assert.AreEqual(20, plateau.Coordinates.YAxis.MaxValue);
            Assert.IsTrue(plateau.Coordinates.IsValid());

            plateau.Initialize(20, 10);
            Assert.AreEqual(10, plateau.Coordinates.XAxis.MaxValue);
            Assert.AreEqual(20, plateau.Coordinates.YAxis.MaxValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlateauTests_AddRobotToUninitializedPlateau_ExceptionExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            Assert.IsNotNull(plateau);
            var robot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(robot);
        }

        [TestMethod]
        [ExpectedException(typeof(PlateauNotInitializedException))]
        public void PlateauTests_AddRobotWithSelfMadeCoodinatesToUninitializedPlateau_ExceptionExpected()
        {
            var plateau = Plateau.Instance;
            Assert.IsNotNull(plateau);
            var robot = new Robot(new MarsCoordinates(10,10), 0, 0);
            plateau.AddRobot(robot);
        }

        [TestMethod]
        public void PlateauTests_AddRobot_RobotIsAdded()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            Assert.IsNotNull(plateau);

            var robot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(robot);
            Assert.AreEqual("0 0 N", robot.ToString());
            Assert.AreEqual("0 0 N", plateau.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(RobotPlacementException))]
        public void PlateauTests_AddRobotTwiceToTheSameLocation_ExceptionExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            Assert.IsNotNull(plateau);

            var robot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(robot);

            var anotherRobot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(anotherRobot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlateauTests_BadInitializationXValue_ExceptionExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(-10, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlateauTests_BadInitializationYValue_ExceptionExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, -10);
        }
    }

    
}
