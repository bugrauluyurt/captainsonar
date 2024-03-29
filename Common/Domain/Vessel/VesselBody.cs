﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public class VesselBody(List<Room> rooms)
    {
        public readonly List<Room> Rooms = rooms;

        public bool IsAllRoomsDamaged()
        {
            return Rooms?.All(room => room.IsAllRoomUnitsDamaged()) ?? false;
        }

        public bool IsAllNuclearRoomUnitsDamaged()
        {
            return Rooms?.All(room => room.IsAllNuclearRoomUnitsDamaged()) ?? false;
        }
    }
}
