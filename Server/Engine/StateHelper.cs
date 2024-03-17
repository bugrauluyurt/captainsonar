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

        public static State AddPlayerToTeam(State state, TeamName team, Player player)
        {
            state.TeamState[team].Players.Add(player);
            return state;
        }

        public static State StartGame(State state, Player? player)
        {
            var playerInTurn = (player ?? state.TeamState[TeamName.Team1].Players.First()) ?? throw new InvalidOperationException("Player is required to start the game");

            state.Status = GameStatus.InProgress;

            state.Turn = new Turn
            {
                Team = TeamName.Team1,
                Player = playerInTurn
            };

            return state;
        }

        public static State FinishGame(State state, TeamName victor)
        {
            state.Status = GameStatus.Finished;
            state.Victor = victor;
            return state;
        }

        public static State NextTurn(State state)
        {
            // @TODO: Turn might still stay at the current team if the team needs to take another action.
            // The system needs to do a validation here.
            var team1 = state.TeamState[TeamName.Team1];
            var team2 = state.TeamState[TeamName.Team2];

            if (state.Turn?.Team == TeamName.Team1)
            {
                state.Turn = new Turn
                {
                    Team = TeamName.Team2,
                    Player = team2.Players.First()
                };
            }
            else
            {
                state.Turn = new Turn
                {
                    Team = TeamName.Team1,
                    Player = team1.Players.First()
                };
            }

            return state;
        }
    }
}
