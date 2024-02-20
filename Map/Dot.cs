using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static CaptainSonar.Pathfinder;

namespace CaptainSonar.Map
{

    internal class Dot(Coordinate location, bool hasObstacle) : Node(location, null, 0, 0)
    {
        // public readonly Coordinate Location;
        public readonly bool HasObstacle = hasObstacle;

        public override string ToString()
        {
            return $"{Location.Row}:{Location.Column}";
        }

        public static string GetReadableCoordinate(Coordinate location)
        {
            return $"{location.Row + 1}:{MapHelpers.ColumnsAlphabetical[location.Column]}";
        }
    }
}
