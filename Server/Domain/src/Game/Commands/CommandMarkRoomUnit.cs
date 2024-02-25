using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Assets;

namespace CaptainSonar.Game.Commands
{
    internal class CommandMarkRoomUnitData
    {
        public string RoomUnitPositionId { get; } = "";
    }

    internal class CommandMarkRoomUnit(CommandMarkRoomUnitData data) : Command<CommandMarkRoomUnitData>(data, CommandName.MarkRoomUnit)
    {
    }
}
