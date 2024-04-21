using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Map
{
    public static class Pathfinder
    {

        public static List<Coordinate>? FindShortestPath(Dot[,] matrix, Coordinate start, Coordinate end)
        {
            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            var startNode = new Node(start, null, 0, Heuristic(start, end));

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var currentNode = GetLowestFScoreNode(openSet);

                if (currentNode.Location.Equals(end))
                {
                    return ReconstructPath(currentNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbor in GetNeighbors(matrix, currentNode.Location))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = currentNode.GScore + 1;

                    if (!openSet.Contains(neighbor) || tentativeGScore < neighbor.GScore)
                    {
                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);

                        neighbor.Parent = currentNode;
                        neighbor.GScore = tentativeGScore;
                        neighbor.FScore = neighbor.GScore + Heuristic(neighbor.Location, end);
                    }
                }
            }

            // No path found
            return null;
        }

        public static int FindShortestPathCount(Dot[,] matrix, Coordinate start, Coordinate end)
        {
            if (start.ToString() == end.ToString())
            {
                return 0;
            }

            if (start.Row == end.Row)
            {
                return Math.Abs(start.Column - end.Column);
            }

            if (start.Column == end.Column)
            {
                return Math.Abs(start.Row - end.Row);
            }

            var shortestPath = FindShortestPath(matrix, start, end);

            if (shortestPath == null)
            {
                return -1;
            }

            return shortestPath.Count - 1;
        }

        private static Node GetLowestFScoreNode(List<Node> openSet)
        {
            Node lowestNode = openSet[0];
            foreach (var node in openSet)
            {
                if (node.FScore < lowestNode.FScore)
                    lowestNode = node;
            }
            return lowestNode;
        }

        private static List<Coordinate> ReconstructPath(Node? node)
        {
            var path = new List<Coordinate>();
            while (node != null)
            {
                path.Add(node.Location);
                node = node?.Parent;
            }
            path.Reverse();
            return path;
        }

        private static int Heuristic(Coordinate a, Coordinate b)
        {
            // Simple Manhattan distance heuristic
            return Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column);
        }

        private static IEnumerable<Node> GetNeighbors(Dot[,] matrix, Coordinate location)
        {
            var neighbors = new List<Node>();

            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            // Define possible moves (up, down, left, right)
            int[][] moves = [[-1, 0], [1, 0], [0, -1], [0, 1]];

            foreach (var move in moves)
            {
                int newRow = location.Row + move[0];
                int newColumn = location.Column + move[1];

                if (newRow >= 0 && newRow < rowCount && newColumn >= 0 && newColumn < columnCount &&
                    !matrix[newRow, newColumn].Props.HasObstacle)
                {
                    neighbors.Add(new Node(new Coordinate(newRow, newColumn), null, 0, 0));
                }
            }

            return neighbors;
        }


    }

    public class Node(Coordinate location, Node? parent, int gScore, int hScore)
    {
        public Coordinate Location = location;
        public Node? Parent = parent;
        public int GScore = gScore;
        public int FScore = gScore + hScore;
        public GridSection Section = MapHelpers.GetGridSectionFromCoordinate(location);
    }
}
