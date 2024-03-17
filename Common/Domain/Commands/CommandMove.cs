using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandMoveData
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }

    public class CommandMove(CommandMoveData data) : Command<CommandMoveData>(data, CommandName.Move)
    {
    }
}
