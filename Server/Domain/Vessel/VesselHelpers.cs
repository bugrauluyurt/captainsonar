using CaptainSonar.Assets;
using CaptainSonar.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    class VesselHelpers
    {
        private static readonly Dictionary<RoomPosition, Direction> RoomPositionDirections = new()
        {
            { RoomPosition.Front, Direction.West },
            { RoomPosition.FrontMiddle, Direction.North },
            { RoomPosition.RearMiddle, Direction.South },
            { RoomPosition.Rear, Direction.East }
        };

        private static readonly Dictionary<VesselType, Dictionary<RoomPosition, RoomUnit[]>> SystemVessels = new() {
            {
                VesselType.Submarine, new Dictionary<RoomPosition, RoomUnit[]>()
                {
                    {
                        RoomPosition.Front, [
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP1)}", AssetType.Attack, RoomUnitType.Yellow),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP2)}", AssetType.Passive, RoomUnitType.Yellow),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP3)}", AssetType.Radar, RoomUnitType.Yellow),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP4a)}", AssetType.Radar, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP4b)}", AssetType.Nuclear, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Front, RoomUnitPosition.UP4c)}", AssetType.Nuclear, null),
                        ]
                    },
                    {
                        RoomPosition.FrontMiddle, [
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP1)}", AssetType.Passive, RoomUnitType.Orange),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP2)}", AssetType.Attack, RoomUnitType.Orange),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP3)}", AssetType.Passive, RoomUnitType.Orange),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP4a)}", AssetType.Radar, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP4b)}", AssetType.Attack, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.FrontMiddle, RoomUnitPosition.UP4c)}", AssetType.Nuclear, null),
                        ]
                    },
                    {
                        RoomPosition.RearMiddle, [
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP1)}", AssetType.Radar, RoomUnitType.Black),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP2)}", AssetType.Passive, RoomUnitType.Black),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP3)}", AssetType.Attack, RoomUnitType.Black),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP4a)}", AssetType.Attack, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP4b)}", AssetType.Nuclear, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.RearMiddle, RoomUnitPosition.UP4c)}", AssetType.Passive, null),
                        ]
                    },
                    {
                        RoomPosition.Rear, [
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP1)}", AssetType.Radar, RoomUnitType.Orange),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP2)}", AssetType.Passive, RoomUnitType.Black),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP3)}", AssetType.Attack, RoomUnitType.Yellow),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP4a)}", AssetType.Nuclear, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP4b)}", AssetType.Radar, null),
                            new RoomUnit($"{GetRoomUnitPositionId(RoomPosition.Rear, RoomUnitPosition.UP4c)}", AssetType.Nuclear, null),
                        ]
                    }
                }
            }
        };

        public static Vessel CreateVessel(VesselType vesselType)
        {
            var systemVessels = GetSystemVessel(vesselType);
            List<Room> rooms = [];
            foreach (var roomPosition in systemVessels.Keys)
            {
                var direction = GetRoomDirectionByPosition(roomPosition) ?? throw new Exception("Invalid room position");
                var room = new Room(direction, roomPosition);
                room.SetRoomUnits(systemVessels[roomPosition]);

                rooms.Add(room);
            }
            return new Vessel(rooms);
        }

        public static Dictionary<RoomPosition, RoomUnit[]> GetSystemVessel(VesselType vesselType)
        {
            if (!SystemVessels.TryGetValue(vesselType, out Dictionary<RoomPosition, RoomUnit[]>? value))
            {
                throw new Exception("Vessel type not found");
            }

            return value;
        }

        public static Direction? GetRoomDirectionByPosition(RoomPosition roomPosition)
        {
            return RoomPositionDirections.TryGetValue(roomPosition, out Direction value) ? value : null;
        }


        // @INFO: Intentionally kept separate from the RoomUnit class
        public static string GetRoomUnitPositionId(RoomPosition roomPosition, RoomUnitPosition roomUnitPosition)
        {
            return $"{roomPosition}:{roomUnitPosition}";
        }

        public static RoomPosition GetRoomPositionByRoomUnitPositionId(string roomUnitPositionId)
        {
            return (RoomPosition)Enum.Parse(typeof(RoomPosition), roomUnitPositionId.Split(":")[0]);
        }

        public static RoomUnitPosition GetRoomUnitPositionByRoomUnitPositionId(string roomUnitPositionId)
        {
            return (RoomUnitPosition)Enum.Parse(typeof(RoomUnitPosition), roomUnitPositionId.Split(":")[1]);
        }
    }
}
