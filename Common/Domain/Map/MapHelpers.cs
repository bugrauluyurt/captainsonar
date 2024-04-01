using CaptainSonar.Common.Domain.Game;
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

        public static bool CanMove(
            Coordinate coordinateProspect,
            GridType mapType,
            List<Dot> dots)
        {
            return IsCoordinateInBounds(coordinateProspect, mapType) &&
                   !IsCoordinateOnObstacle(coordinateProspect, mapType) &&
                   !IsCoordinateOnPath(coordinateProspect, dots);
        }

        public static bool IsCoordinateOnAnyTeamsMine(Coordinate coordinate, Grid grid)
        {
            var dot = grid.GetDot(coordinate);
            return dot.IsMineExist(TeamName.Team1) || dot.IsMineExist(TeamName.Team2);
        }

        public static bool IsCoordinateOnATeamMine(Coordinate coordinate, Grid grid, TeamName teamName)
        {
            var dot = grid.GetDot(coordinate);
            return dot.IsMineExist(teamName);
        }

    }
}
