using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandSessionStartData
    {
        public Player Player { get; set; } = null!;
    }

    public class CommandSessionStart(CommandSessionStartData data) : Command<CommandSessionStartData>(CommandName.Session_Start, data)
    {
    }
}