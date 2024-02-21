using CaptainSonar.Assets;
using CaptainSonar.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    internal class RoomUnit(AssetType type, Direction direction, bool isMain, RoomUnitType? roomUnitType)
    {
        private readonly AssetType _type = type;
        private readonly Direction _unitDirection = direction;
        private readonly RoomUnitType? _roomUnitType = roomUnitType;
        private readonly bool IsMain = isMain;
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

        public bool IsInRoom(Direction direction)
        {
            return _unitDirection == direction;
        }
    }
}
