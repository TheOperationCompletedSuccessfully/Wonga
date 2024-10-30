using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;
using Wonga.Data;

namespace Wonga
{
    class Program
    {
        private static readonly Regex _plateauCoordsRegex = new Regex(@"^\s*(?<x>[\d]+)\s+(?<y>[\d]+)\s*$", RegexOptions.Compiled);
        private static readonly Regex _robotCoordsRegex = new Regex(@"^\s*(?<x>[\d]+)\s+(?<y>[\d]+)\s+(?<d>[\w])\s*$", RegexOptions.Compiled);
        private static readonly Regex _robotCommandsRegex = new Regex(@"^\s*(?<commands>[RLM]+)\s*", RegexOptions.Compiled);
        static readonly Queue<string> _outputMessages = new Queue<string>();
        static readonly List<object> _liveAndDeadRobots = new List<object>();
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Initialize();
            ReadPlateauCoordinates();
            while(ReadRobot()) {}
            WriteAllMessages();
            bool seeAliveOnly = Convert.ToBoolean(ConfigurationManager.AppSettings["WriteAliveRobotsOnly"]);
            WritePlateau(seeAliveOnly);
            Console.ReadKey();
        }

        private static void Initialize()
        {
            XmlConfigurator.Configure();
        }

        private static void WritePlateau(bool seeAliveOnly)
        {
            if (seeAliveOnly)
            {
                //to see only live robots
                Console.WriteLine(Plateau.Instance.ToString());
            }
            else
            {
                //to see live and dead robots(dead robots = exceptions what happened)
                foreach (var robot in _liveAndDeadRobots)
                {
                    Console.WriteLine(robot.ToString());
                }
            }
        }

        private static void WriteAllMessages()
        {
            while (_outputMessages.Count > 0)
            {
                Console.WriteLine(_outputMessages.Dequeue());
            }
        }

        private static void LogMessage(string message)
        {
            _outputMessages.Enqueue(message);
            Log.Error(message);
        }

        private static void ReadPlateauCoordinates()
        {
            var coordinateString = Console.ReadLine();
            if (string.IsNullOrEmpty(coordinateString))
            {
                LogMessage("Null or empty string for plateau detected");
                return;
            }

            try
            {
                var match = _plateauCoordsRegex.Match(coordinateString);
                if (!match.Success)
                {
                    LogMessage("Wrong format of plateau coordinates");
                    return;
                }
                int x = int.Parse(match.Groups["x"].Value);
                int y = int.Parse(match.Groups["y"].Value);
                Plateau.Instance.Initialize(x, y);
            }
            catch (RegexMatchTimeoutException)
            {
                LogMessage("Plateau coordinates parsing takes too long");
            }
            catch (OverflowException)
            {
                LogMessage("Plateau coordinates too big");
            }
            catch (Exception ex)
            {
                LogMessage(ex.ToString());
            }
        }

        private static bool ReadRobot()
        {
            var robotCoords = Console.ReadLine();
            if (string.IsNullOrEmpty(robotCoords))
            {
                return false;
            }

            var robot = PlaceRobot(robotCoords);
            MoveRobot(robot);

            return true;
        }

        private static void MoveRobot(Robot robot)
        {
            if (robot == null)
            {
                return;
            }

            var robotMoveLine = Console.ReadLine();
            if (string.IsNullOrEmpty(robotMoveLine))
            {
                LogMessage("Null or empty string for plateau detected");
                return;
            }

            try
            {
                var match = _robotCommandsRegex.Match(robotMoveLine);
                if (!match.Success)
                {
                    LogMessage("Wrong format of robot commands");
                    return;
                }
                string commands = match.Groups["commands"].Value;
                for (int i = 0; i < commands.Length; i++)
                {
                    var command = commands.Substring(i, 1);
                    var moveAction = (MoveAction)Enum.Parse(typeof(MoveAction), command);
                    robot.ExecuteCommand(moveAction);
                }
            }
            catch (Exception ex)
            {
                _liveAndDeadRobots[_liveAndDeadRobots.Count - 1] = ex;

            }
       }

        private static Robot PlaceRobot(string robotCoords)
        {
            Robot robot = null;
            try
            {
                var match = _robotCoordsRegex.Match(robotCoords);
                if (!match.Success)
                {
                    LogMessage("Wrong format of robot coordinates");
                    return null;
                }
                int x = int.Parse(match.Groups["x"].Value);
                int y = int.Parse(match.Groups["y"].Value);
                var d = (Direction)Enum.Parse(typeof(Direction), match.Groups["d"].Value);

                robot = new Robot(Plateau.Instance.Coordinates, x, y, d);
                Plateau.Instance.AddRobot(robot);
                _liveAndDeadRobots.Add(robot);
            }
            catch (Exception ex)
            {
                _liveAndDeadRobots.Add(ex);
            }
            
            return robot;
        }
    }
}
