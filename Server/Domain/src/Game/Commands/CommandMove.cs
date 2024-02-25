using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Game.Commands
{
    class CommandMoveData
    {
        public required int Column { get; set; }
        public required int Row { get; set; }
    }
    internal class CommandMove(CommandMoveData data) : Command<CommandMoveData>(data, CommandName.Move)
    {
    }
}
