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
        private static readonly Dictionary<VesselType, Dictionary<int, RoomUnit[]>> SystemVessels = new() {
            {
                VesselType.Submarine, new Dictionary<int, RoomUnit[]>()
                {
                    {
                        0, [
                            new RoomUnit(AssetType.Attack, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit(AssetType.Passive, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit(AssetType.Radar, Direction.West, true, RoomUnitType.Yellow),
                            new RoomUnit(AssetType.Radar, Direction.West, false, null),
                            new RoomUnit(AssetType.Nuclear, Direction.West, false, null),
                            new RoomUnit(AssetType.Nuclear, Direction.West, false, null),
                            ]
                    },
                    {
                        1, [
                            new RoomUnit(AssetType.Passive, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit(AssetType.Attack, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit(AssetType.Passive, Direction.North, true, RoomUnitType.Orange),
                            new RoomUnit(AssetType.Radar, Direction.North, false, null),
                            new RoomUnit(AssetType.Attack, Direction.North, false, null),
                            new RoomUnit(AssetType.Nuclear, Direction.North, false, null),
                            ]
                    },
                    {
                        2, [
                            new RoomUnit(AssetType.Radar, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit(AssetType.Passive, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit(AssetType.Attack, Direction.South, true, RoomUnitType.Black),
                            new RoomUnit(AssetType.Attack, Direction.South, false, null),
                            new RoomUnit(AssetType.Nuclear, Direction.South, false, null),
                            new RoomUnit(AssetType.Passive, Direction.South, false, null),
                            ]
                    },
                    {
                        3, [
                            new RoomUnit(AssetType.Radar, Direction.East, true, RoomUnitType.Orange),
                            new RoomUnit(AssetType.Passive, Direction.East, true, RoomUnitType.Black),
                            new RoomUnit(AssetType.Attack, Direction.East, true, RoomUnitType.Yellow),
                            new RoomUnit(AssetType.Nuclear, Direction.East, false, null),
                            new RoomUnit(AssetType.Radar, Direction.East, false, null),
                            new RoomUnit(AssetType.Nuclear, Direction.East, false, null),
                            ]
                    }
                }
            }
        };

        public static Dictionary<VesselType, Dictionary<int, RoomUnit[]>> GetSystemVessels()
        {
            return SystemVessels;
        }
    }
}
