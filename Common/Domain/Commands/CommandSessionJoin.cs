using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandSessionJoinData
    {
        public Player Player { get; set; } = null!;
        public TeamName TeamName { get; set; }
    }

    public class CommandSessionJoin(CommandSessionJoinData data) : Command<CommandSessionJoinData>(CommandName.Session_Join, data)
    {
    }
}