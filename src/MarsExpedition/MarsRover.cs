using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsExpedition.Interfaces;

namespace MarsExpedition
{
    public class MarsRover : IMarsRover
    {
        public Point Location { get; protected set; }

        protected Point InnerDirection { get; set; }

        protected string InnerId { get; set; }

        public string Id => InnerId;

        public string Direction
        {
            get
            {
                if (InnerDirection.X == 0 && InnerDirection.Y == 1)
                    return "N";
                if (InnerDirection.X == 1 && InnerDirection.Y == 0)
                    return "E";
                if (InnerDirection.X == 0 && InnerDirection.Y == -1)
                    return "S";
                if (InnerDirection.X == -1 && InnerDirection.Y == 0)
                    return "W";

                throw new ArgumentException($"{InnerDirection} is not a supported direction !");
            }
        }

        protected IMarsMap Map { get; }

        protected internal MarsRover(string id, Point initialLocation, string direction, IMarsMap marsMap)
        {
            if (string.IsNullOrEmpty((id ?? string.Empty).Trim()))
                throw new ArgumentNullException(nameof(id));
            if (initialLocation == null)
                throw new ArgumentNullException(nameof(initialLocation));

            InnerId = id;
            Location = initialLocation;
            InnerDirection = GetDirectionFromString(direction);

            Map = marsMap ?? throw new ArgumentNullException(nameof(marsMap));
        }

        protected Point GetDirectionFromString(string direction)
        {
            switch (direction)
            {
                case "N":
                    return new Point(0, 1);
                case "E":
                    return new Point(1, 0);
                case "S":
                    return new Point(0, -1);
                case "W":
                    return new Point(-1, 0);
                default:
                    throw new ArgumentException($"{direction} is not a supported direction !");
            }
        }

        public void ExecuteCommands(string commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'M':
                        Move();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                }
            }
        }

        public void Move()
        {
            var newLocation = Point.Add(Location, new Size(InnerDirection));
            if ((newLocation.X >= 0 && newLocation.Y >= 0) && (newLocation.X <= Map.MaxGridPoint.X && newLocation.Y <= Map.MaxGridPoint.Y))
            {
                var oldRover = this.MemberwiseClone() as IMarsRover;
                Location = newLocation;
                Map.UpdateRover(oldRover, this);
            }
        }

        public void TurnLeft()
        {
            InnerDirection = InnerDirection.X != 0 ?
                new Point(InnerDirection.Y, InnerDirection.X) :
                new Point(InnerDirection.Y * -1, InnerDirection.X * -1);
        }

        public void TurnRight()
        {
            InnerDirection = InnerDirection.X != 0 ?
                new Point(InnerDirection.Y * -1, InnerDirection.X * -1) :
                new Point(InnerDirection.Y, InnerDirection.X);
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder
                .Append(Location.X)
                .Append(" ")
                .Append(Location.Y)
                .Append(" ")
                .Append(Direction);

            return strBuilder.ToString();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Map.UpdateRover(this, this, true);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
