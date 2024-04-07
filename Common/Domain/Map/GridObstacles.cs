using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Map
{
    public static class GridObstacles
    {
        private static readonly Dictionary<GridType, int[][]> ObstacleByGridType = new()
        {
            { GridType.Alpha, [
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
            // @TODO: Add the rest of the grids here Ex: (Bravo, Charlie)
        };

        public static int[][] GetObstaclesByGridType(GridType gridType)
        {
            return ObstacleByGridType[gridType];
        }
    }
}
