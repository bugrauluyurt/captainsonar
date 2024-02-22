using CaptainSonar.Assets;
using CaptainSonar.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    internal class RoomUnit(string positionId, AssetType type, Direction direction, bool isMain, RoomUnitType? roomUnitType)
    {
        public readonly string PositionId = positionId;
        private readonly AssetType _type = type;
        private readonly Direction _unitDirection = direction;
        private readonly RoomUnitType? _roomUnitType = roomUnitType;
        public readonly bool IsMain = isMain;
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

        public bool IsInRoom(Direction direction)
        {
            return _unitDirection == direction;
        }
    }
}
