using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CaptainSonar.Map
{
    // @TODO: Carry this into the Map namespace
    internal enum MapType
    {
        Alpha,
        Bravo,
        Charlie,
    }

    internal class Helpers
    {
        public static string[] ColumnsAlphabetical = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"];

        const int RowCount = 15;
        const int ColumnCount = 15;

        static readonly Dictionary<MapType, int[][]> Obstacles = new()
        {
            { MapType.Alpha, [
                [1, 2], [1, 6], [1, 12], [1, 13],
                [2, 2], [2, 8], [2, 12],
                [3, 8],
                [6, 1], [6, 3], [6, 6], [6, 8],
                [7, 1], [7, 3], [7, 6],
                [8, 3], [8, 7], [8, 11], [8, 12], [8, 13],
                [10, 3],
                [11, 2], [11, 7], [11, 11],
                [12, 0], [12, 12],
                [13, 2], [13, 6], [13, 8], [13, 13],
                [14, 3]
            ] },
        };

        public static int[][] GetObstaclesByMapType(MapType mapType)
        {
            return Obstacles[mapType];
        }

        public static int[] GetDimensionsByMapType(MapType mapType)
        {
            return new int[] { RowCount, ColumnCount };
        }
    }
}
