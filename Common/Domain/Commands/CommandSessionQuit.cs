using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandSessionQuitData
    {
        public Player Player { get; set; } = null!;
    }

    public class CommandSessionQuit(CommandSessionQuitData data) : Command<CommandSessionQuitData>(CommandName.Session_Quit, data)
    {
    }
}