using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandMarkRoomUnitData
    {
        public string RoomUnitPositionId { get; } = "";
    }

    public class CommandMarkRoomUnit(CommandMarkRoomUnitData data) : Command<CommandMarkRoomUnitData>(data, CommandName.MarkRoomUnit)
    {
    }
}
