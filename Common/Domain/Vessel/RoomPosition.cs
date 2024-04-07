using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public enum RoomPosition
    {
        Front,
        FrontMiddle,
        RearMiddle,
        Rear,
    }

    public static class RoomPositionExtensions
    {
        public static bool IsRoomPositionValid(string roomPosition)
        {
            return Enum.TryParse<RoomPosition>(roomPosition, out _);
        }
    }
}
