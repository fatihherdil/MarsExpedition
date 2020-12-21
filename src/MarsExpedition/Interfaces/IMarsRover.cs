using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsExpedition.Interfaces
{
    public interface IMarsRover : IDisposable
    {
        Point Location { get; }

        string Id { get; }

        string Direction { get; }

        void ExecuteCommands(string commands);
        void Move();
        void TurnLeft();
        void TurnRight();
    }
}
