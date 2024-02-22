using CaptainSonar.Assets;
using CaptainSonar.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    class Helpers
    {
        private static readonly Dictionary<VesselType, Dictionary<RoomPosition, RoomUnit[]>> SystemVessels = new() {
            {
                VesselType.Submarine, new Dictionary<RoomPosition, RoomUnit[]>()
                {
                    {
                        RoomPosition.Front, [
                            new RoomUnit($"{RoomPosition.Front}:a", AssetType.Attack, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit($"{RoomPosition.Front}:b", AssetType.Passive, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit($"{RoomPosition.Front}:c", AssetType.Radar, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit($"{RoomPosition.Front}:d", AssetType.Radar, Direction.West, false, null),
                            new RoomUnit($"{RoomPosition.Front}:e", AssetType.Nuclear, Direction.West, false, null),
                            new RoomUnit($"{RoomPosition.Front}:f", AssetType.Nuclear, Direction.West, false, null),
                            ]
                    },
                    {
                        RoomPosition.FrontMiddle, [
                            new RoomUnit($"{RoomPosition.FrontMiddle}:a", AssetType.Passive, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit($"{RoomPosition.FrontMiddle}:b", AssetType.Attack, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit($"{RoomPosition.FrontMiddle}:c", AssetType.Passive, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit($"{RoomPosition.FrontMiddle}:d", AssetType.Radar, Direction.North, false, null),
                            new RoomUnit($"{RoomPosition.FrontMiddle}:e", AssetType.Attack, Direction.North, false, null),
                            new RoomUnit($"{RoomPosition.FrontMiddle}:f", AssetType.Nuclear, Direction.North, false, null),
                            ]
                    },
                    {
                        RoomPosition.RearMiddle, [
                            new RoomUnit($"{RoomPosition.RearMiddle}:a", AssetType.Radar, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit($"{RoomPosition.RearMiddle}:b", AssetType.Passive, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit($"{RoomPosition.RearMiddle}:c", AssetType.Attack, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit($"{RoomPosition.RearMiddle}:d", AssetType.Attack, Direction.South, false, null),
                            new RoomUnit($"{RoomPosition.RearMiddle}:e", AssetType.Nuclear, Direction.South, false, null),
                            new RoomUnit($"{RoomPosition.RearMiddle}:f", AssetType.Passive, Direction.South, false, null),
                            ]
                    },
                    {
                        RoomPosition.Rear, [
                            new RoomUnit($"{RoomPosition.Rear}:a", AssetType.Radar, Direction.East, true, RoomUnitType.Orange),
                            new RoomUnit($"{RoomPosition.Rear}:b", AssetType.Passive, Direction.East, true, RoomUnitType.Black),
                            new RoomUnit($"{RoomPosition.Rear}:c", AssetType.Attack, Direction.East, true, RoomUnitType.Yellow),
                            new RoomUnit($"{RoomPosition.Rear}:d", AssetType.Nuclear, Direction.East, false, null),
                            new RoomUnit($"{RoomPosition.Rear}:e", AssetType.Radar, Direction.East, false, null),
                            new RoomUnit($"{RoomPosition.Rear}:f", AssetType.Nuclear, Direction.East, false, null),
                            ]
                    }
                }
            }
        };

        public static new Dictionary<RoomPosition, RoomUnit[]> GetSystemVessel(VesselType vesselType)
        {
            if (!SystemVessels.TryGetValue(vesselType, out Dictionary<RoomPosition, RoomUnit[]>? value))
            {
                throw new Exception("Vessel type not found");
            }

            return value;
        }

        public static bool IsAllNuclearRoomsDamaged(Vessel vessel)
        {
            return vessel.Rooms.All(room =>
            {
                return room.GetRoomUnits().All(roomUnit => roomUnit.IsDamaged());
            });
        }
    }
}
