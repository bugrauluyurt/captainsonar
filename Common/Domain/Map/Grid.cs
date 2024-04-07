using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Map
{
    public class Grid
    {
        private readonly Dot[,] _dots;

        public readonly GridType MapType;
        public readonly int[] Dimensions;
        public readonly int[][] Obstacles;

        public Grid(GridType mapType)
        {
            MapType = mapType;
            Dimensions = MapHelpers.GetDimensionsByGridType(mapType);
            Obstacles = GridObstacles.GetObstaclesByGridType(mapType);
            _dots = CreateDots(Dimensions[0], Dimensions[1], Obstacles);
        }

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
                    matrix[row, column] = new Dot(coordinate, new DotProps { HasObstacle = hasObstacle });
                }
            }
            return matrix;
        }

        public Dot GetDot(Coordinate coordinate)
        {
            return _dots[coordinate.Row, coordinate.Column];
        }

        public Dot[,] GetDots()
        {
            return _dots;
        }

        public void PrintGrid(List<Dot>? dotsMarked = null, bool isSectionViewEnabled = false)
        {
            dotsMarked ??= [];

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
                Console.Write(MapHelpers.ColumnsAlphabetical[i] + " ");
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
                    var printedNonObstacle = isSectionViewEnabled ? ((int)dot.Section).ToString() : ".";
                    // If the dot is marked, print * instead of .
                    if (dotsMarked.Any(d => d.Location.Row == i && d.Location.Column == j))
                    {
                        printedNonObstacle = "O";
                    }
                    var printValue = dot.Props.HasObstacle ? "X" : printedNonObstacle;
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
