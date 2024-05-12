using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Common.Domain.Vessel;

namespace CaptainSonar.Server.Engine
{
    public enum GameStatus
    {
        NotStarted,
        InProgress,
        Finished
    }

    public class StateInfo
    {
        public required Coordinate? Location { get; set; } = null;
        public string Text { get; set; } = "";
    }

    public class StateMine
    {
        public required Dot Dot { get; init; }
    }

    public class TeamState
    {
        public List<Dot> Dots { get; set; } = [];
        public IEnumerable<Asset> Assets { get; init; } = [];
        public VesselBody Vessel { get; init; } = null!;
        public List<Player> Players { get; } = []; // The players in the team.
        public List<string> Notes { get; } = []; // the list of notes the player is taking.
        public List<StateMine> Mines { get; } = []; // the list of mines the player has set.
        public List<StateInfo> Info { get; set; } = []; // the list of information text which can be tied to dots.
    }

    public class Turn
    {
        public TeamName Team { get; set; }
    }

    public class State
    {
        public Dictionary<TeamName, TeamState> TeamState { get; init; } = [];
        public TeamName? Victor { get; set; }
        public Turn? Turn { get; set; }
        public GameStatus Status { get; set; } = GameStatus.NotStarted;
        public Grid Grid { get; init; } = null!;
    }
}
