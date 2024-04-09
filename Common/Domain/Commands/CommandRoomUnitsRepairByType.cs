using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Vessel;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandRoomUnitsRepairByTypeData
    {
        public TeamName TeamName { get; set; }
        public RoomUnitType RoomUnitType { get; set; }
    }

    public class CommandRoomUnitsRepairByType(CommandRoomUnitsRepairByTypeData data) : Command<CommandRoomUnitsRepairByTypeData>(CommandName.RoomUnits_RepairByType, data)
    {
    }
}