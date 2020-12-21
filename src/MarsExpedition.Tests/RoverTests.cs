using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NUnit.Framework;

namespace MarsExpedition.Tests
{
    public class RoverTests
    {
        private TestMarsMap _marsMarsMap;

        [SetUp]
        public void TestSetup()
        {
            int height = 10, width = 10;
            _marsMarsMap = TestMarsMap.CreateTestMap(height, width);
        }

        [Test]
        [TestCase(0, 0, "N", 0, 1)]
        [TestCase(0, 0, "E", 1, 0)]
        [TestCase(0, 0, "S", 0, -1)]
        [TestCase(0, 0, "W", -1, 0)]
        public void CreateRoverTests(int x, int y, string direction, int expectedDirectionX, int expectedDirectionY)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);
            Assert.AreEqual(rover.Location.X, x);
            Assert.AreEqual(rover.Location.Y, y);

            Assert.AreEqual(rover.DirectionPoint.X, expectedDirectionX);
            Assert.AreEqual(rover.DirectionPoint.Y, expectedDirectionY);
        }

        [Test]
        [TestCase(0, 0, "N")]
        [TestCase(0, 0, "E")]
        [TestCase(0, 2, "S")] //Rover can't go past boundries
        [TestCase(2, 0, "W")] //Rover can't go past boundries
        public void RoverDirectionMoveTests(int x, int y, string direction)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);

            var locationX = rover.Location.X;
            var locationY = rover.Location.Y;

            rover.Move();
            Assert.AreEqual(rover.Location.X, locationX + rover.DirectionPoint.X);
            Assert.AreEqual(rover.Location.Y, locationY + rover.DirectionPoint.Y);

            locationX = rover.Location.X;
            locationY = rover.Location.Y;

            rover.Move();
            Assert.AreEqual(rover.Location.X, locationX + rover.DirectionPoint.X);
            Assert.AreEqual(rover.Location.Y, locationY + rover.DirectionPoint.Y);
        }

        [Test]
        [TestCase(0, 0, "N", 1, 0)]
        [TestCase(0, 0, "E", 0, -1)]
        [TestCase(0, 0, "S", -1, 0)]
        [TestCase(0, 0, "W", 0, 1)]
        public void RoverTurnRightTests(int x, int y, string direction, int expectedDirectionX, int expectedDirectionY)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);

            rover.TurnRight();
            Assert.AreEqual(rover.DirectionPoint.X, expectedDirectionX);
            Assert.AreEqual(rover.DirectionPoint.Y, expectedDirectionY);
        }

        [Test]
        [TestCase(0, 0, "N", -1, 0)]
        [TestCase(0, 0, "E", 0, 1)]
        [TestCase(0, 0, "S", 1, 0)]
        [TestCase(0, 0, "W", 0, -1)]
        public void RoverTurnLeftTests(int x, int y, string direction, int expectedDirectionX, int expectedDirectionY)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);

            rover.TurnLeft();
            Assert.AreEqual(rover.DirectionPoint.X, expectedDirectionX);
            Assert.AreEqual(rover.DirectionPoint.Y, expectedDirectionY);
        }

        [Test]
        [TestCase(0, 0, "N", new[] { "R" }, 1, 0)]
        [TestCase(0, 0, "N", new[] { "L" }, -1, 0)]
        [TestCase(0, 0, "N", new[] { "R", "R" }, 0, -1)]
        [TestCase(0, 0, "N", new[] { "L", "L" }, 0, -1)]
        [TestCase(0, 0, "N", new[] { "R", "R", "R" }, -1, 0)]
        [TestCase(0, 0, "N", new[] { "L", "L", "L" }, 1, 0)]
        [TestCase(0, 0, "N", new[] { "R", "R", "R", "R" }, 0, 1)]
        [TestCase(0, 0, "N", new[] { "L", "L", "L", "L" }, 0, 1)]
        public void RoverDirectionTests(int x, int y, string direction, string[] directions, int expectedDirectionX, int expectedDirectionY)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);
            foreach (var strDirection in directions)
            {
                switch (strDirection)
                {
                    case "R":
                        rover.TurnRight();
                        break;
                    case "L":
                        rover.TurnLeft();
                        break;
                }
            }

            Assert.AreEqual(rover.DirectionPoint.X, expectedDirectionX);
            Assert.AreEqual(rover.DirectionPoint.Y, expectedDirectionY);
        }


        [Test]
        [TestCase(0, 0, "N", "M", 0, 1, 0, 1, "N")]
        [TestCase(0, 0, "N", "MR", 1, 0, 0, 1, "E")]
        [TestCase(0, 0, "N", "MRR", 0, -1, 0, 1, "S")]
        [TestCase(0, 0, "N", "MRRR", -1, 0, 0, 1, "W")]
        [TestCase(0, 0, "N", "MRRR", -1, 0, 0, 1, "W")]
        [TestCase(0, 0, "N", "MMMMMMMMMMMMM", 0, 1, 0, 10, "N")]
        [TestCase(0, 0, "N", "RMMMMMMMMMMMMM", 1, 0, 10, 0, "E")]
        public void RoverCommandTests(int x, int y, string direction, string commands, int expectedDirectionX, int expectedDirectionY, int expectedLocationX, int expectedLocationY, string expectedDirection)
        {
            var rover = _marsMarsMap.CreateTestRover(x, y, direction);

            rover.ExecuteCommands(commands);
            Assert.AreEqual(rover.Location.X, expectedLocationX);
            Assert.AreEqual(rover.Location.Y, expectedLocationY);
            Assert.AreEqual(rover.DirectionPoint.X, expectedDirectionX);
            Assert.AreEqual(rover.DirectionPoint.Y, expectedDirectionY);
            Assert.AreEqual(rover.Direction, expectedDirection);
        }
    }

    public class TestMarsMap : MarsMap
    {
        protected TestMarsMap(Point maxGridPoints) : base(maxGridPoints)
        {
        }

        public static TestMarsMap CreateTestMap(int height, int width)
        {
            return new TestMarsMap(new Point(width, height));
        }

        public TestMarsRover CreateTestRover(int x, int y, string direction)
        {
            var id = Guid.NewGuid().ToString("N");
            var rover = TestMarsRover.CreateTestRover(id, x, y, direction, this);

            InnerRovers[id] = rover;

            return rover;
        }
    }

    public class TestMarsRover : MarsRover
    {
        public Point DirectionPoint => InnerDirection;

        protected TestMarsRover(string id, Point initialLocation, string innerDirection, MarsMap marsMap) : base(id, initialLocation, innerDirection, marsMap)
        {
        }

        public static TestMarsRover CreateTestRover(string id, int x, int y, string direction, TestMarsMap marsMarsMap)
        {
            return new TestMarsRover(id, new Point(x, y), direction, marsMarsMap);
        }
    }
}
