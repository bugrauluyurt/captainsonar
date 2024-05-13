using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandRoomUnitDamageData
    {
        public required TeamName TeamName { get; set; }
        public required string RoomUnitPositionId { get; set; } = null!;
    }

    public class CommandRoomUnitDamage(CommandRoomUnitDamageData data) : Command<CommandRoomUnitDamageData>(CommandName.RoomUnit_Damage, data)
    {
    }
}