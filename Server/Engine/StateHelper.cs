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
    internal class StateHelper
    {
        public static State CreateState()
        {
            TeamState team1State = new();
            TeamState team2State = new();

            team1State.Vessel = VesselHelpers.CreateVessel(VesselType.Submarine);
            team2State.Vessel = VesselHelpers.CreateVessel(VesselType.Submarine);

            var state = new State
            {
                // Session is null during creation.
                // Victor is null during creation.
                // Turn is null during creation.
                TeamState = new Dictionary<TeamName, TeamState>
                {
                    { TeamName.Team1, team1State },
                    { TeamName.Team2, team2State }
                },
                Status = GameStatus.NotStarted
            };

            return state;
        }

        // AddPlayer => Adds a player to a team
        // StartGame => Sets the turn
    }
}
