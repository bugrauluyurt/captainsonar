using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Map
{
    internal class Map
    {
        private readonly Dot[,] _dots;

        public readonly MapType MapType;
        public readonly int[] Dimensions;
        public readonly int[][] Obstacles;

        public Map(MapType mapType)
        {
            MapType = mapType;
            Dimensions = Helpers.GetDimensionsByMapType(mapType);
            Obstacles = Helpers.GetObstaclesByMapType(mapType);
            _dots = CreateDots(Dimensions[0], Dimensions[1], Obstacles);
        }

        public Dot[,] Dots => _dots;

        static public Dot[,] CreateDots(int columnCount, int rowCount, int[][] obstacles)
        {
            var matrix = new Dot[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    var coordinate = new Coordinate(row, column);
                    // Not optimal, but it's a small matrix
                    var hasObstacle = obstacles.Any(obstacle => obstacle[0] == row && obstacle[1] == column);
                    matrix[row, column] = new Dot(coordinate, hasObstacle);
                }
            }
            return matrix;
        }

        public void PrintMap()
        {
            
            if (_dots == null)
            {
                throw new Exception("Map not initialized");
            }

            static void PrintBoundaries(int columnCount)
            {
                var initialPaddingCount = 2;
                for (int i = 0; i < columnCount * 2 + initialPaddingCount; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }

            // Print start boundary
            PrintBoundaries(_dots.GetLength(1));

            // Print the column headers
            Console.Write("   ");

            for (int i = 0; i < _dots.GetLength(1); i++)
            {
                Console.Write(Helpers.ColumnsAlphabetical[i] + " ");
            }

            // Move to the next line after headers
            Console.WriteLine();

            // Rows
            for (int i = 0; i < _dots.GetLength(0); i++)
            {
                // Print the row number
                Console.Write((i + 1).ToString("D2") + " ");

                // Columns
                for (int j = 0; j < _dots.GetLength(1); j++)
                {
                    var dot = _dots[i, j];
                    var printValue = dot.HasObstacle ? "X" : ".";
                    Console.Write(printValue + " ");
                }
                // Move to the next line after each row
                Console.WriteLine();
            }

            // Print end boundary
            PrintBoundaries(_dots.GetLength(1));
        }
    }
}
