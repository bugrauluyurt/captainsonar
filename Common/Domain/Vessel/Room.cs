﻿using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Vessel
{
    public class Room(Direction direction, RoomPosition position)
    {
        public readonly RoomPosition Position = position;
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

        public bool IsAnyRoomUnitDamaged(AssetType assetType)
        {
            return GetRoomUnits().Any(roomUnit => roomUnit.GetAssetType() == assetType && roomUnit.IsDamaged());
        }

        public bool IsAllUnlinkedRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.RoomUnitType == RoomUnitType.Unlinked).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllLinkedRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.IsLinked()).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllNuclearRoomUnitsDamaged()
        {
            return _roomUnits?.Where(roomUnit => roomUnit.GetAssetType() == AssetType.Nuclear).All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public bool IsAllRoomUnitsDamaged()
        {
            return _roomUnits?.All(roomUnit => roomUnit.IsDamaged()) ?? false;
        }

        public static bool IsRoomPositionValid(string roomPositionString)
        {
            return Enum.TryParse<RoomPosition>(roomPositionString, out _);
        }

        public void RepairRoomUnitByPositionId(string positionId)
        {
            var roomUnit = _roomUnits?.FirstOrDefault(roomUnit => roomUnit.PositionId == positionId);
            roomUnit?.Repair();
        }

        public void RepairRoomUnitsByRoomUnitType(RoomUnitType roomUnitType)
        {
            _roomUnits?.Where(roomUnit => roomUnit.RoomUnitType == roomUnitType).ToList().ForEach(roomUnit => roomUnit.Repair());
        }

        public void RepairAllUnlinkedRoomUnits()
        {
            _roomUnits?.Where(roomUnit => roomUnit.RoomUnitType == RoomUnitType.Unlinked).ToList().ForEach(roomUnit => roomUnit.Repair());
        }

        public void DamageRoomUnitByPositionId(string positionId)
        {
            var roomUnit = _roomUnits?.FirstOrDefault(roomUnit => roomUnit.PositionId == positionId);
            roomUnit?.Damage();
        }


    }
}
