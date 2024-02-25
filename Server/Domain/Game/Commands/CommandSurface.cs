using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Game.Commands
{
    internal class CommandSurfaceData
    {
        public required int Column { get; set; }
        public required int Row { get; set; }
    }

    internal class CommandSurface() : Command<CommandSurfaceData>(null, CommandName.Surface)
    {
    }
}
