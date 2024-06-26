﻿using CaptainSonar.Common.Domain.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Map
{
    public static class MapHelpers
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
            if (0 <= row && row < 5 && (0 <= column & column < 5))
            {
                return GridSection.One;
            }

            if (0 <= row && row < 5 && (5 <= column & column < 10))
            {
                return GridSection.Two;
            }

            if (0 <= row && row < 5 && (10 <= column & column < 15))
            {
                return GridSection.Three;
            }

            if (5 <= row && row < 10 && (0 <= column & column < 5))
            {
                return GridSection.Four;
            }

            if (5 <= row && row < 10 && (5 <= column & column < 10))
            {
                return GridSection.Five;
            }

            if (5 <= row && row < 10 && (10 <= column & column < 15))
            {
                return GridSection.Six;
            }

            if (10 <= row && row < 15 && (0 <= column & column < 5))
            {
                return GridSection.Seven;
            }

            if (10 <= row && row < 15 && (5 <= column & column < 10))
            {
                return GridSection.Eight;
            }

            if (10 <= row && row < 15 && (10 <= column & column < 15))
            {
                return GridSection.Nine;
            }

            return GridSection.None;
        }

        public static bool IsCoordinateInBounds(Coordinate coordinate, GridType gridType)
        {
            var dimensions = GetDimensionsByGridType(gridType);
            var rowCount = dimensions[0];
            var columnCount = dimensions[1];
            return coordinate.Row >= 0 && coordinate.Row < rowCount && coordinate.Column >= 0 && coordinate.Column < columnCount;
        }

        public static bool IsCoordinateOnPath(Coordinate coordinate, List<Dot> dots)
        {
            return dots.Any(dot => dot.Location.Row == coordinate.Row && dot.Location.Column == coordinate.Column);
        }

        public static bool IsCoordinateOnObstacle(Coordinate coordinate, GridType mapType)
        {
            var obstacles = GridObstacles.GetObstaclesByGridType(mapType);
            return obstacles.Any(obstacle => obstacle[0] == coordinate.Row && obstacle[1] == coordinate.Column);
        }

        public static bool IsCoordinateAdjacent(Coordinate coordinate1, Coordinate coordinate2)
        {
            return Math.Abs(coordinate1.Row - coordinate2.Row) <= 1 && Math.Abs(coordinate1.Column - coordinate2.Column) <= 1;
        }

        public static bool IsCoordinateListAdjacent(List<Coordinate> coordinates)
        {
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                if (!IsCoordinateAdjacent(coordinates[i], coordinates[i + 1]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsCoordinateListValid(List<Coordinate> coordinatesJumped, GridType gridType, List<Dot> dots)
        {
            return coordinatesJumped.All(coordinate =>
            {
                return IsCoordinateInBounds(coordinate, gridType) &&
                       !IsCoordinateOnObstacle(coordinate, gridType) &&
                       !IsCoordinateOnPath(coordinate, dots);

            });
        }

        public static bool IsCoordinateWithinAllowedDistance(Dot[,] dots, Coordinate coordinate1, Coordinate coordinate2, int distance)
        {
            var shortestPathDistance = Pathfinder.FindShortestPathCount(dots, coordinate1, coordinate2);
            return shortestPathDistance > 0 && shortestPathDistance <= distance;
        }

        public static bool CanMove(
            Coordinate coordinateProspect,
            GridType mapType,
            List<Dot> dots)
        {
            return IsCoordinateInBounds(coordinateProspect, mapType) &&
                   !IsCoordinateOnObstacle(coordinateProspect, mapType) &&
                   !IsCoordinateOnPath(coordinateProspect, dots) &&
                   IsCoordinateAdjacent(dots.Last().Location, coordinateProspect);
        }

        public static Coordinate GetNextCoordinateFromDirection(Coordinate lastKnownCoordinate, Direction direction)
        {
            var row = lastKnownCoordinate.Row;
            var column = lastKnownCoordinate.Column;

            return direction switch
            {
                Direction.North => new Coordinate(row - 1, column),
                Direction.East => new Coordinate(row, column + 1),
                Direction.South => new Coordinate(row + 1, column),
                Direction.West => new Coordinate(row, column - 1),
                _ => new Coordinate(row, column),
            };
        }

        public static Dot GetDotFromCoordinate(Coordinate coordinate, Grid grid)
        {
            return grid.GetDots()[coordinate.Row, coordinate.Column];
        }

    }
}
