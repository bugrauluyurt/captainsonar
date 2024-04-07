using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandRoomUnitsRepairData
    {
        public TeamName TeamName { get; set; }
        public List<string> RoomUnitPositionIds { get; set; } = null!;
    }

    public class CommandRoomUnitsRepair(CommandRoomUnitsRepairData data) : Command<CommandRoomUnitsRepairData>(CommandName.RoomUnits_Repair, data)
    {
    }
}