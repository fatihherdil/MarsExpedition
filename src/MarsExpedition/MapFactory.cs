using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsExpedition.Interfaces;

namespace MarsExpedition
{
    public sealed class MapFactory
    {
        public static IMarsMap CreateMap(int height, int width)
        {
            return new MarsMap(new Point(width, height));
        }
    }
}
