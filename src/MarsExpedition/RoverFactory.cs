using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsExpedition.Interfaces;

namespace MarsExpedition
{
    internal sealed class RoverFactory
    {
        public static IMarsRover CreateRover(string id, int x, int y, string direction, IMarsMap marsMap)
        {
            return new MarsRover(id, new Point(x, y), direction, marsMap);
        }
    }
}
