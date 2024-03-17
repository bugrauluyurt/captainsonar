using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Game;
using CaptainSonar.Vessel;
using CaptainSonar.Map;
using CaptainSonar.Assets;

namespace CaptainSonar.Engine
{
    internal enum GameStatus
    {
        NotStarted,
        InProgress,
        Finished
    }

    internal class TeamState
    {
        public List<Dot> Dots { get; set; } = [];
        public IEnumerable<Asset> Assets { get; set; } = [];
        public VesselBody? Vessel { get; set; } = null;
        public List<Player> Players { get; } = []; // The players in the team.
        public List<string> Notes { get; } = []; // the list of notes the player is taking.
        public List<Dot> DotsInfo { get; set; } = []; // the list of dots that are marked for information purposes. These dots can be enenmy location or any other.
    }

    internal class Turn
    {
        public TeamName Team { get; set; }
        public Player? Player { get; set; } = null;
    }

    internal class State
    {
        public Session? Session { get; set; }
        public Dictionary<TeamName, TeamState> TeamState { get; set; } = [];
        public TeamName? Victor { get; set; }
        public Turn? Turn { get; set; }
        public GameStatus Status { get; set; } = GameStatus.NotStarted;
    }
}
