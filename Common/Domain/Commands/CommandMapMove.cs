using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandMapMoveData
    {
        public TeamName TeamName { get; set; }
        public Direction Direction { get; set; }
    }

    public class CommandMapMove(CommandMapMoveData data) : Command<CommandMapMoveData>(CommandName.Map_Move, data)
    {
    }
}