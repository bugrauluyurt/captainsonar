using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Game;
using static CaptainSonar.Map.Pathfinder;

namespace CaptainSonar.Map
{
    internal class DotMine(Coordinate location, TeamName owner)
    {
        public Coordinate Location { get; set; } = location;
        public TeamName Owner { get; set; } = owner;
    }
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
        public readonly List<DotMine> Mines = [];
        public readonly List<string> Notes = []; // user can store a list of notes for each dot.
        public string Color { get; set; } = "transparent"; // this is mostly used for the client side to show the color of the dot. Anything can be stored here.

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
