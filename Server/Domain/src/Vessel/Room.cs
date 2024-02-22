﻿using CaptainSonar.Assets;
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
        private IEnumerable<RoomUnit> _roomUnits = new RoomUnit[6];

        public Direction GetDirection()
        {
            return _direction;
        }

        public IEnumerable<RoomUnit> GetRoomUnits()
        {
            return _roomUnits;
        }

        public void SetRoomUnits(IEnumerable<RoomUnit> roomUnits)
        {
            _roomUnits = roomUnits;
        }

        public bool IsRoomUnitDamaged(AssetType assetType)
        {
            return GetRoomUnits()?.Any(roomUnit => roomUnit.GetAssetType() == assetType && roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllUnlinkedRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.GetRoomUnitType() == null).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllMainRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.IsMain).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllNuclearRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.GetAssetType() == AssetType.Nuclear).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllRoomUnitsDamaged()
        {
            return _roomUnits?.All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public void RepairRoomUnitByPositionId(string positionId)
        {
            var roomUnit = _roomUnits?.FirstOrDefault(roomUnit => roomUnit.PositionId == positionId);
            roomUnit?.Repair();
        }

        public void RepairAllUnlinkedRoomUnits()
        {
            _roomUnits?.Where(roomUnit => roomUnit.GetRoomUnitType() == null).ToList().ForEach(roomUnit => roomUnit.Repair());
        }

        public void DamageRoomUnitByPositionId(string positionId)
        {
            var roomUnit = _roomUnits?.FirstOrDefault(roomUnit => roomUnit.PositionId == positionId);
            roomUnit?.Damage();
        }
    }
}
