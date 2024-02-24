using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    internal class VesselFactory
    {
        public static Vessel CreateVessel(VesselType vesselType)
        {
            var systemVessels = Helpers.GetSystemVessel(vesselType);
            List<Room> rooms = [];
            foreach (var roomPosition in systemVessels.Keys)
            {
                var direction = Helpers.GetRoomDirectionByPosition(roomPosition) ?? throw new Exception("Invalid room position");
                var room = new Room(direction, roomPosition);
                room.SetRoomUnits(systemVessels[roomPosition]);

                rooms.Add(room);
            }
            return new Vessel(rooms);
        }
    }
}
