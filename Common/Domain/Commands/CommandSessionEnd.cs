using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandSessionEndData
    {
        public Player Player { get; set; } = null!;
    }

    public class CommandSessionEnd(CommandSessionEndData data) : Command<CommandSessionEndData>(CommandName.Session_End, data)
    {
    }
}