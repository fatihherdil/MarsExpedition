using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsExpedition.Interfaces
{
    public interface IMarsMap : IDisposable
    {
        Point MaxGridPoint { get; }

        IReadOnlyList<IMarsRover> Rovers { get; }

        void UpdateRover(IMarsRover oldRover, IMarsRover rover, bool isDisposed = false);

        IMarsRover CreateRover(int x, int y, string direction);
    }
}
