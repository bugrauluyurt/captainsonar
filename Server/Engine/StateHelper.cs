using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Utils;
using CaptainSonar.Common.Domain.Vessel;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Server.Engine
{
    static class StateHelper
    {
        public static State CreateState(Grid grid)
        {
            TeamState team1State = new();
            TeamState team2State = new();

            // Team1 state
            team1State.Vessel = VesselHelpers.CreateVessel(VesselType.Submarine);
            team1State.Assets = AssetHelpers.CreateAssets();
            // Team2 state
            team2State.Vessel = VesselHelpers.CreateVessel(VesselType.Submarine);
            team1State.Assets = AssetHelpers.CreateAssets();

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
                Status = GameStatus.NotStarted,
                Grid = grid
            };

            return state;
        }

        public static State AddPlayerToTeam(State state, TeamName team, Player player)
        {
            state.TeamState[team].Players.Add(player);
            return state;
        }

        public static State RemovePlayerFromTeam(State state, TeamName team, Player player)
        {
            state.TeamState[team].Players.Remove(player);
            return state;
        }

        public static State RemovePlayerFromGame(State state, Player player)
        {
            if (state.TeamState[TeamName.Team1].Players.Contains(player))
            {
                state.TeamState[TeamName.Team1].Players.Remove(player);
            }
            else if (state.TeamState[TeamName.Team2].Players.Contains(player))
            {
                state.TeamState[TeamName.Team2].Players.Remove(player);
            }

            return state;
        }

        public static State StartGame(State state)
        {
            state.Status = GameStatus.InProgress;

            state.Turn = new Turn
            {
                Team = TeamName.Team1,
            };

            return state;
        }

        public static State FinishGame(State state, TeamName victor)
        {
            state.Status = GameStatus.Finished;
            state.Victor = victor;
            return state;
        }

        public static State MoveTeam(State state, TeamName team, Coordinate lastKnownCoordinate, Direction direction)
        {
            var newCoordinate = MapHelpers.GetNextCoordinateFromDirection(lastKnownCoordinate, direction);
            var grid = state.Grid;
            var dot = MapHelpers.GetDotFromCoordinate(newCoordinate, grid);
            state.TeamState[team].Dots.Add(dot.Clone());
            return state;
        }

        /*
        Commands List
        - Session_Start (Player) => The system is going to start the game. The player is going to be the first player of the first team. Create the state and the session.
        - Session_End => Owner of the session ends the game for good.
        - Session_Quit => Player is going to quit the game. Another player can be invited to the game at this point.
        - Session_Join => (SessionID) => Player joins the game with the session id. The player should be invited to the session by the creator. The other player should try to join the session with the SessionID.
        - [DOES NOT AFFECT THE STATE] Session_Invite => (PlayerId, session) => The player is going to invite another player to the game.
        - Map_Move (Team, Direction) => Player is going to send a direction. The system will find the next dot.
        validation =>
            - Check if the dot is valid.
            - The next dot should not be out of boundaries.
            - The player can not move to a dot that has an obstacle.
            - [Optional] If any of the players move on top of the other. The players are moved
            - After a player moves, the system should check the next turn. If the player has not crossed any asset slot or has not crossed any room unit, then the same has the turn,
            otherwise, the turn is next player's.
        - Map_Surface => Team submarine surfaces and same player needs to report the position of his own submarine.
        - Report_AfterSonar => A player is going to report One True, One False location item to the other player.
        - Report_AfterDrone => A player is going to report the section of the map to the other player after a drone.
        - Report_AfterSurface => Player reports the position of himself.
        - Game_End_Turn => The player ends the turn.
        - RoomUnit_Damage
        - AssetSlots_Increase
        - AssetSlots_Use (data sent changes according to the used asset type)
        - Info_AddDots (user adds info dots on the map to store information about the enemy's location or other things)
         */

        /*
       Commands List
       - STATE MACHINE
           After each command, the system should generate a new state and upsert a diagnostics message into the list.
           In this diagnostics message list, the things has automatically happened should be listed.

           The system should generate an object like this:
           {
               State: State,
               Diagnostics: [
                   {
                       Message: "The player has moved to the next dot.",
                       Data: any
                   },
                   {
                       Message: "Team A's ship is damaged.",
                       Data: any
                   },
                   {
                       Message: "Game ended. Team A won.",
                       Data: any
                   },
                   {
                       Message: "Command is not valid. The player can not move to the specified direction.",
                       Data: any
                   }
               ]
           }

           Here are some examples, we need to develop a chart for every scenario:
           - If the next dot is a mine, the system should damage the other player automatically and write a system diagnostics message. The mine should be removed from the dot object.
           -
        */
    }
}
