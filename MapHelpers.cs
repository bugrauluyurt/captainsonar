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
    internal enum MapType
    {
        Alpha,
        Bravo
    }

    
    
    // @TODO: Rename this to MapHelpers and create a helpers folder
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

        static public int GetDistance(Dot a, Dot b)
        {
            return Math.Abs(a.Location.Row - b.Location.Row) + Math.Abs(a.Location.Column - b.Location.Column);
        }

        static public List<Dot> GetNeighborNodes(Dot dot, Dot[,] grid)
        {
            List<Dot> neighborNodes = new List<Dot>();

            int startX = Math.Max(0, dot.Location.Row - 1);
            int endX = Math.Min(grid.GetLength(0) - 1, dot.Location.Row + 1);
            int startY = Math.Max(0, dot.Location.Column - 1);
            int endY = Math.Min(grid.GetLength(1) - 1, dot.Location.Column + 1);

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (x == dot.Location.Row && y == dot.Location.Column)
                        continue;

                    neighborNodes.Add(grid[x, y]);
                }
            }

            return neighborNodes;
        }

        static public List<Dot>? CalculateShortestDistance(Dot[,] grid, Dot startNode, Dot endNode)
        {
            List<Dot> openList = new List<Dot> { startNode };
            List<Dot> closedList = new List<Dot>();

            while (openList.Count > 0)
            {
                Dot? currentNode = openList.OrderBy(node => node.FCost).First();

                if (currentNode == endNode)
                {
                    // We found the path
                    List<Dot> path = new List<Dot>();
                    while (currentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode?.Parent;
                    }
                    path.Reverse();
                    return path;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var neighborNode in GetNeighborNodes(currentNode, grid))
                {
                    if (closedList.Contains(neighborNode) || neighborNode.HasObstacle)
                        continue;

                    int tentativeGCost = currentNode.GCost + GetDistance(currentNode, neighborNode);
                    if (tentativeGCost < neighborNode.GCost || !openList.Contains(neighborNode))
                    {
                        neighborNode.Parent = currentNode;
                        neighborNode.GCost = tentativeGCost;
                        neighborNode.HCost = GetDistance(neighborNode, endNode);

                        if (!openList.Contains(neighborNode))
                            openList.Add(neighborNode);
                    }
                }
            }

            // No path could be found
            return null;
        }

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
            Console.Write("  ");

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
