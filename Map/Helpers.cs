using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
            return [RowCount, ColumnCount];
        }

        public static MapSection GetMapSectionFromCoordinate(Coordinate coordinate)
        {
            var column = coordinate.Column;
            var row = coordinate.Row;
            if ((0 <= row && row < 5) && (0 <= column & column < 5))
            {
                return MapSection.One;
            }

            if ((0 <= row && row < 5) && (5 <= column & column < 10))
            {
                return MapSection.Two;
            }

            if ((0 <= row && row < 5) && (10 <= column & column < 15))
            {
                return MapSection.Three;
            }

            if ((5 <= row && row < 10) && (0 <= column & column < 5))
            {
                return MapSection.Four;
            }

            if ((5 <= row && row < 10) && (5 <= column & column < 10))
            {
                return MapSection.Five;
            }

            if ((5 <= row && row < 10) && (10 <= column & column < 15))
            {
                return MapSection.Six;
            }

            if ((10 <= row && row < 15) && (0 <= column & column < 5))
            {
                return MapSection.Seven;
            }

            if ((10 <= row && row < 15) && (5 <= column & column < 10))
            {
                return MapSection.Eight;
            }

            if ((10 <= row && row < 15) && (10 <= column & column < 15))
            {
                return MapSection.Nine;
            }

            return MapSection.None;
        }

        public static bool CanMove(Coordinate coordinate, MapType mapType)
        {
            var obstacles = GetObstaclesByMapType(mapType);
            var row = coordinate.Row;
            if (row < 0 || row >= RowCount)
            {
                return false;
            }

            var column = coordinate.Column;
            if (column < 0 || column >= ColumnCount)
            {
                return false;
            }
            
            return !obstacles.Any(obstacle => obstacle[0] == coordinate.Row && obstacle[1] == coordinate.Column);
        }
    }
}
