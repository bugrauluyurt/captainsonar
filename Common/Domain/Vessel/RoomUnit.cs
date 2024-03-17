using CaptainSonar.Common.Domain.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public class RoomUnit(string positionId, AssetType type, RoomUnitType? roomUnitType)
    {
        public readonly string PositionId = positionId;
        private readonly AssetType _type = type;
        private readonly RoomUnitType? _roomUnitType = roomUnitType;
        private bool _isDamaged = false;

        public AssetType GetAssetType()
        {
            return _type;
        }

        public RoomUnitType? GetRoomUnitType()
        {
            return _roomUnitType;
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
            return _roomUnitType == null;
        }

        public bool IsLinked() => !IsUnlinked();
    }
}
