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

    class Helpers
    {
        public static string[] ColumnsAlphabetical = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"];

        const int RowCount = 15;
        const int ColumnCount = 15;

        public static string GetReadableCoordinate(Coordinate coordinate)
        {
            var column = ColumnsAlphabetical[coordinate.Column];
            var row = coordinate.Row + 1;
            return $"{column}{row}";
        }

        public static int[] GetDimensionsByGridType(GridType gridType)
        {
            return [RowCount, ColumnCount];
        }

        public static GridSection GetGridSectionFromCoordinate(Coordinate coordinate)
        {
            var column = coordinate.Column;
            var row = coordinate.Row;
            if ((0 <= row && row < 5) && (0 <= column & column < 5))
            {
                return GridSection.One;
            }

            if ((0 <= row && row < 5) && (5 <= column & column < 10))
            {
                return GridSection.Two;
            }

            if ((0 <= row && row < 5) && (10 <= column & column < 15))
            {
                return GridSection.Three;
            }

            if ((5 <= row && row < 10) && (0 <= column & column < 5))
            {
                return GridSection.Four;
            }

            if ((5 <= row && row < 10) && (5 <= column & column < 10))
            {
                return GridSection.Five;
            }

            if ((5 <= row && row < 10) && (10 <= column & column < 15))
            {
                return GridSection.Six;
            }

            if ((10 <= row && row < 15) && (0 <= column & column < 5))
            {
                return GridSection.Seven;
            }

            if ((10 <= row && row < 15) && (5 <= column & column < 10))
            {
                return GridSection.Eight;
            }

            if ((10 <= row && row < 15) && (10 <= column & column < 15))
            {
                return GridSection.Nine;
            }

            return GridSection.None;
        }

        // @TODO: CanMove should be moved to a different class
        public static bool CanMove(Coordinate coordinate, GridType mapType)
        {
            var obstacles = GridObstacles.GetObstaclesByGridType(mapType);
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
            
            // @TODO: Player can not move onto his/her own path. The system needs to check it here.
            return !obstacles.Any(obstacle => obstacle[0] == coordinate.Row && obstacle[1] == coordinate.Column);
        }
    }
}
