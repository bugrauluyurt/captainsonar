using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public class VesselBody(List<Room> rooms)
    {
        public readonly List<Room> Rooms = rooms;

        public bool IsAllRoomsDamaged()
        {
            return Rooms.All(room => room.IsAllRoomUnitsDamaged());
        }

        public bool IsAllNuclearRoomUnitsDamaged()
        {
            return Rooms.All(room => room.IsAllNuclearRoomUnitsDamaged());
        }

        public void RepairAllRooms()
        {
            Rooms.ForEach(room => room.GetRoomUnits().ToList().ForEach(roomUnit => roomUnit.Repair()));
        }

        public Room FindRoomRoomByPosition(RoomPosition position)
        {
            return Rooms.Find(room => room.Position == position) ?? throw new Exception("Room not found");
        }

        public void DamageRoomUnitByPositionId(string roomUnitPositionId)
        {
            var roomPosition = VesselHelpers.GetRoomPositionByRoomUnitPositionId(roomUnitPositionId);
            var room = FindRoomRoomByPosition(roomPosition);
            room.DamageRoomUnitByPositionId(roomUnitPositionId);
        }

        public void RepairRoomUnitByPositionId(string roomUnitPositionId)
        {
            var roomPosition = VesselHelpers.GetRoomPositionByRoomUnitPositionId(roomUnitPositionId);
            var room = FindRoomRoomByPosition(roomPosition);
            room.RepairRoomUnitByPositionId(roomUnitPositionId);
        }

        public void RepairRoomUnitsByPositionIds(List<string> roomUnitPositionIds)
        {
            roomUnitPositionIds.ForEach(RepairRoomUnitByPositionId);
        }
    }
}
