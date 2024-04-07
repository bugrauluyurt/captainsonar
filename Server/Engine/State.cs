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

    public class StateDotInfo
    {
        public required Dot Dot { get; set; }
        public string Info { get; set; } = "";
    }

    public class StateMine
    {
        public required Dot Dot { get; set; }
    }

    public class TeamState
    {
        public List<Dot> Dots { get; set; } = [];
        public IEnumerable<Asset> Assets { get; set; } = [];
        public VesselBody? Vessel { get; set; } = null;
        public List<Player> Players { get; } = []; // The players in the team.
        public List<string> Notes { get; } = []; // the list of notes the player is taking.
        public List<StateMine> Mines { get; } = []; // the list of mines the player has set.
        public List<StateDotInfo> DotsInfo { get; set; } = []; // the list of dots that are marked for information purposes. These dots can be enenmy location or any other.
    }

    public class Turn
    {
        public TeamName Team { get; set; }
    }

    public class State
    {
        public Dictionary<TeamName, TeamState> TeamState { get; set; } = [];
        public TeamName? Victor { get; set; }
        public Turn? Turn { get; set; }
        public GameStatus Status { get; set; } = GameStatus.NotStarted;
        public Grid Grid { get; init; } = null!;
    }
}
