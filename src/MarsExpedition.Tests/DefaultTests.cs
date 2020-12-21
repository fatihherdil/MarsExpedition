using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarsExpedition.Tests
{
    public class DefaultTests
    {
        private static IEnumerable<IList<TestInfo>> Rovers()
        {
            var list = new List<TestInfo>
            {
                new TestInfo { MapHeight = 10, MapWidth = 10,
                    Rovers = new RoverInfo[]
                    {
                        new RoverInfo { StartLocationX = 0, StartLocationY = 0, StartDirection = "N", Commands = "RMML", ExpectedOutput = "2 0 N" },
                        new RoverInfo { StartLocationX = 0, StartLocationY = 0, StartDirection = "N", Commands = "RMMLMM", ExpectedOutput = "2 2 N" },
                        new RoverInfo { StartLocationX = 0, StartLocationY = 0, StartDirection = "N", Commands = "RMMLMMR", ExpectedOutput = "2 2 E" },
                        new RoverInfo { StartLocationX = 0, StartLocationY = 0, StartDirection = "N", Commands = "RMMLMMRR", ExpectedOutput = "2 2 S" },
                        new RoverInfo { StartLocationX = 0, StartLocationY = 0, StartDirection = "N", Commands = "RMMLMML", ExpectedOutput = "2 2 W" },
                    } },
            };

            return new[] {list};
        }

        [Test, TestCaseSource("Rovers")]
        public void DefaultTest(IList<TestInfo> tests)
        {
            foreach (var test in tests)
            {
                using (var map = MapFactory.CreateMap(test.MapHeight, test.MapWidth))
                {
                    foreach (var rover in test.Rovers)
                    {
                        var createdRover = map.CreateRover(rover.StartLocationX, rover.StartLocationY, rover.StartDirection);
                        createdRover.ExecuteCommands(rover.Commands);
                        Assert.AreEqual(createdRover.ToString(), rover.ExpectedOutput);
                    }
                }
            }
        }
    }

    public class TestInfo
    {
        public RoverInfo[] Rovers { get; set; }
        public int MapHeight { get; set; }
        public int MapWidth { get; set; }
    }

    public class RoverInfo
    {
        public int StartLocationX { get; set; }
        public int StartLocationY { get; set; }
        public string StartDirection { get; set; }
        public string Commands { get; set; }

        public string ExpectedOutput { get; set; }
    }
}
