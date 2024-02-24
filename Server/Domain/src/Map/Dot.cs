using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CaptainSonar.Map.Pathfinder;

namespace CaptainSonar.Map
{
    internal class DotProps
    {
        public bool HasObstacle { get; set; }
        public bool IsPositionResetAllowed { get; set; }

        public DotProps()
        {
            HasObstacle = false;
            IsPositionResetAllowed = true;
        }
    }

    internal class Dot(Coordinate location, GridSection section, DotProps? props) : Node(location, null, 0, 0)
    {
        public readonly DotProps Props = props ?? new DotProps();
        public readonly GridSection Section = section;

        public override string ToString()
        {
            return $"{Location.Row}:{Location.Column}";
        }

        public static string GetReadableLocation(Coordinate location)
        {
            return MapHelpers.GetReadableCoordinate(location);
        }
    }
}
