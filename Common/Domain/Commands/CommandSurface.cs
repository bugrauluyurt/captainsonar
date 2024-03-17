using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandSurfaceData
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }

    public class CommandSurface() : Command<CommandSurfaceData>(null, CommandName.Surface)
    {
    }
}
