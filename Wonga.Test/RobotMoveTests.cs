using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wonga.Data;
using Wonga.Data.Exceptions;

namespace Wonga.Test
{
    [TestClass]
    public class RobotMoveTests
    {
        [ClassInitialize]
        public static void RobotMoveTestsInitialize(TestContext context)
        {
            XmlConfigurator.Configure();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PlateauForTest.InstanceForTest.UnInitialize();
        }

        [TestMethod]
        public void RobotMoveTests_RobotTurnedLeft_NoException()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(robot);
            robot.TurnLeft();
            Assert.AreEqual("0 0 W", robot.ToString());
        }

        [TestMethod]
        public void RobotMoveTests_RobotTurnedRight_NoException()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 0, 0, Direction.E);
            plateau.AddRobot(robot);
            robot.TurnRight();
            Assert.AreEqual("0 0 S", robot.ToString());
        }

        [TestMethod]
        public void RobotMoveTests_RobotMovedNorth_CoordinatesAsExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(robot);
            Assert.AreEqual(Direction.N, robot.Direction);
            robot.Move();
            Assert.AreEqual("0 1 N", robot.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(CoordinatesOverflowException))]
        public void RobotMoveTests_RobotTurnedSouthAndMovedOusidePlateau_ExceptionExpected()
        {
            var plateau = PlateauForTest.InstanceForTest;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 0, 0, Direction.E);
            plateau.AddRobot(robot);
            Assert.AreEqual(Direction.E, robot.Direction);
            robot.TurnRight();
            Assert.AreEqual(Direction.S, robot.Direction);
            robot.Move();
        }

        [TestMethod]
        public void RobotMoveTests_SecondRobotTriesToMoveToFirstRobotLocation_MoveIsNotPerformed()
        {
            var plateau = Plateau.Instance;
            plateau.Initialize(10, 10);

            var firstRobot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(firstRobot);
            firstRobot.Move();
            Assert.AreEqual("0 1 N", firstRobot.ToString());

            var secondRobot = new Robot(plateau.Coordinates, 0, 0);
            plateau.AddRobot(secondRobot);
            secondRobot.Move();

            Assert.AreEqual("0 0 N", secondRobot.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(CoordinatesOverflowException))]
        public void RobotMoveTests_TwoRobotsCanGoOursideAsExpected_ExceptionExpected()
        {
            var plateau = Plateau.Instance;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 1, 0, Direction.S);
            plateau.AddRobot(robot);
            try
            {
                robot.Move();
            }
            catch (CoordinatesOverflowException)
            {
                //first robot is lost
            }

            var anotherRobot = new Robot(plateau.Coordinates, 1, 0, Direction.S);
            plateau.AddRobot(anotherRobot);
            robot.Move();
            //second robot should be successfully lost
        }

        [TestMethod]
        public void RobotMoveTests_RobotMakesCircle_NoException()
        {
            var plateau = Plateau.Instance;
            plateau.Initialize(10, 10);

            var robot = new Robot(plateau.Coordinates, 2, 1);
            plateau.AddRobot(robot);
            robot.ExecuteCommand(MoveAction.M);
            robot.ExecuteCommand(MoveAction.R);
            robot.ExecuteCommand(MoveAction.M);
            robot.ExecuteCommand(MoveAction.R);
            robot.ExecuteCommand(MoveAction.M);
            robot.ExecuteCommand(MoveAction.R);
            robot.ExecuteCommand(MoveAction.M);
            
            Assert.AreEqual("2 1 W", robot.ToString());
        }
    }
}
