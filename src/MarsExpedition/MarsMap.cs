using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsExpedition.Interfaces;

namespace MarsExpedition
{
    public class MarsMap : IMarsMap
    {
        private const string roverFormat = "{0}_{1}";
        public Point MaxGridPoint { get; protected set; }
        protected Dictionary<string, IMarsRover> InnerRovers { get; set; }

        protected Dictionary<string, IMarsRover> RoverCollision { get; set; }

        public IReadOnlyList<IMarsRover> Rovers => InnerRovers.Values.ToList();

        protected internal MarsMap(Point maxGridPoints)
        {
            MaxGridPoint = maxGridPoints;
            InnerRovers = new Dictionary<string, IMarsRover>();
            RoverCollision = new Dictionary<string, IMarsRover>();
        }

        public IMarsRover CreateRover(int x, int y, string direction)
        {
            var id = Guid.NewGuid().ToString("N");
            var rover = RoverFactory.CreateRover(id, x, y, direction, this);

            InnerRovers[id] = rover;
            CheckForCollisionAndLog(rover);

            return rover;
        }

        void CheckForCollisionAndLog(IMarsRover rover)
        {
            var roverKey = string.Format(roverFormat, rover.Location.X, rover.Location.Y);

            if (RoverCollision.TryGetValue(roverKey, out var collisionRover))
            {
                Debug.WriteLine(
                    $"Collision Detected!\nRover In Position Id:{collisionRover.Id}\nIncoming Rover Id:{rover.Id}");
            }
            else
            {
                RoverCollision[roverKey] = rover;
            }
        }

        public void UpdateRover(IMarsRover oldRover, IMarsRover rover, bool isDisposed = false)
        {
            if (isDisposed)
            {
                InnerRovers.Remove(rover.Id);
                var roverKey = string.Format(roverFormat, rover.Location.X, rover.Location.Y);
                RoverCollision[roverKey] = null;
                return;
            }
            CheckForCollisionAndLog(rover);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var rovers in InnerRovers.Values)
                {
                    rovers.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
