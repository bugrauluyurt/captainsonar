using CaptainSonar.Common.Domain.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public class RoomUnit(string positionId, AssetType type, RoomUnitType roomUnitType)
    {
        public readonly string PositionId = positionId;
        private readonly AssetType _type = type;
        public readonly RoomUnitType RoomUnitType = roomUnitType;
        private bool _isDamaged = false;

        public AssetType GetAssetType()
        {
            return _type;
        }

        public void Damage()
        {
            _isDamaged = true;
        }

        public void Repair()
        {
            _isDamaged = false;
        }

        public bool IsDamaged()
        {
            return _isDamaged;
        }

        public bool IsUnlinked()
        {
            return RoomUnitType == RoomUnitType.Unlinked;
        }

        public bool IsLinked() => !IsUnlinked();

        public static bool IsRoomUnitPositionIdValid(string roomUnitPositionId)
        {
            string[]? positions = roomUnitPositionId.Split(":");
            string roomPositionString = positions.ElementAtOrDefault(0) ?? "";
            string roomUnitPositionString = positions.ElementAtOrDefault(1) ?? "";
            return VesselHelpers.IsRoomUnitPositionStringValid(roomUnitPositionString) && VesselHelpers.IsRoomPositionStringValid(roomPositionString);
        }
    }
}
