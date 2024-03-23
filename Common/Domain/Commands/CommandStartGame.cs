using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandStartGameData
    {
        public Player Player { get; } = null!;
    }

    public class CommandStartGame(CommandStartGameData data) : Command<CommandStartGameData>(data, CommandName.Session_Start)
    {
    }
}