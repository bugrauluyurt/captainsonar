using CaptainSonar.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    internal class Room(Direction direction)
    {
        private readonly Direction _direction = direction;
        public IEnumerable<RoomUnit>? RoomUnits { get; set; }

        public Direction GetDirection()
        {
            return _direction;
        }

        public IEnumerable<RoomUnit>? GetRoomUnits()
        {
            return RoomUnits;
        }

        public void SetRoomUnits(IEnumerable<RoomUnit> roomUnits)
        {
            RoomUnits = roomUnits;
        }
    }
}
