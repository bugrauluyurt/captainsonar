using CaptainSonar.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CaptainSonar
{
    // @TODO: Carry this into the Map namespace
    internal enum MapType
    {
        Alpha,
        Bravo
    }

    internal class MapHelpers
    {
        const int RowCount = 15;
        const int ColumnCount = 15;

        static public string[] ColumnsAlphabetical = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"];

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

        //static public int CalculateShortestDistance(Dot[,] grid, Dot startNode, Dot endNode)
        //{
        //    return 0;
        //}

        static public Dot[,] CreateDots(MapType mapType, int columnCount = ColumnCount, int rowCount = RowCount)
        {
            var matrix = new Dot[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    var coordinate = new Coordinate(row, column);
                    // Not optimal, but it's a small matrix
                    var hasObstacle = Obstacles[mapType].Any(obstacle => obstacle[0] == row && obstacle[1] == column);
                    matrix[row, column] = new Dot(coordinate, hasObstacle);
                }
            }
            return matrix;
        }

        public static void PrintMap(Dot[,] dots)
        {
            // Print the column headers
            Console.Write("   ");

            for (int i = 0; i < dots.GetLength(1); i++)
            {
                Console.Write(ColumnsAlphabetical[i] + " ");
            }

            // Move to the next line after headers
            Console.WriteLine();

            // Rows
            for (int i = 0; i < dots.GetLength(0); i++) 
            {
                // Print the row number
                Console.Write((i + 1).ToString("D2") + " ");

                // Columns
                for (int j = 0; j < dots.GetLength(1); j++) 
                {
                    var dot = dots[i, j];
                    var printValue = dot.HasObstacle ? "X" : "o";
                    Console.Write(printValue + " ");
                }
                // Move to the next line after each row
                Console.WriteLine();
            }
        }
    }
}
