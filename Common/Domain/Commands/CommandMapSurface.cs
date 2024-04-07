using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandMapSurfaceData
    {
        public TeamName TeamName { get; set; }
        public Coordinate Coordinate { get; set; } = null!;
    }

    public class CommandMapSurface(CommandMapSurfaceData data) : Command<CommandMapSurfaceData>(CommandName.Map_Surface, data)
    {
    }
}