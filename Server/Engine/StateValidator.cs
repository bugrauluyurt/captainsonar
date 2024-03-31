using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Server.Engine
{
    public static class StateValidator
    {
        public static StateExecutionStep ValidateSessionStart(StateExecutionStep stateExecutionStep, Player player)
        {
            var state = stateExecutionStep.State;
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is not null,
                    1001
                ),
                (
                    state.Victor is null,
                    1002
                ),
                (
                    state.Status == GameStatus.NotStarted,
                    1003
                ),
                (
                    state.TeamState[TeamName.Team1].Players.Count == 0 && state.TeamState[TeamName.Team2].Players.Count == 0,
                    1005
                )
            ], []);
        }

        public static StateExecutionStep ValidateSessionQuit(StateExecutionStep stateExecutionStep, Player player)
        {
            // @INFO: Teams are allowed to have no players. At this point the session owner can send another invitation to another player.
            // @INFO: If the player is the owner of the session, this control is being done at session level not at state level.
            // if (player is not null)
            // {
            //     var team1Players = state.TeamState[TeamName.Team1].Players;
            //     var team2Players = state.TeamState[TeamName.Team2].Players;
            //     if ((!team1Players.Contains(player) && team1Players.Count == 1) || (!team2Players.Contains(player) && team1Players.Count == 1))
            //     {
            //         exceptions.Add(new InvalidOperationException("Player is the last player in the game"));
            //     }
            // }
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is not null,
                    1001
                ),
            ], []);
        }

        public static StateExecutionStep ValidateSessionEnd(StateExecutionStep stateExecutionStep, Player player)
        {
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is not null,
                    1001
                ),
                (
                    stateExecutionStep.State.Victor is not null,
                    1002
                ),
                (
                    stateExecutionStep.State.Status == GameStatus.NotStarted,
                    1004
                )
            ], []);
        }

        public static StateExecutionStep ValidateSessionJoin(StateExecutionStep stateExecutionStep, Player player, TeamName teamName)
        {
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is not null,
                    1001
                ),
                (
                    player is not null && !stateExecutionStep.State.TeamState[teamName].Players.Contains(player),
                    1007
                )
            ], []);
        }

        public static StateExecutionStep ValidateMapMove(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            Coordinate coordinate)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !MapHelpers.IsCoordinateInBounds(coordinate, gridType),
                    1008
                ),
                (
                    !MapHelpers.IsCoordinateOnObstacle(coordinate, gridType),
                    1009
                ),
                (
                    !MapHelpers.IsCoordinateOnPath(coordinate, state.TeamState[teamName].Dots),
                    1010
                ),
                (
                    !MapHelpers.CanMove(coordinate, gridType, state.TeamState[teamName].Dots),
                    1011
                )
            ], []);
        }

        // ValidateMapMove(StateExecutionStep stateExecutionStep, TeamName teamName, Coordinate coordinate)
        // - Next coordinart can not be out of boundaries.
        // - Next coordinate can not be on top of an obstacle. The system should look at the obstacles and check if the next coordinate is an obstacle.
        // - Next coordinate can not belong to any of the player's dots. In other words the player can not move on top of his/her own path.
        // - If next coordinate is on top of a mine, then generate a diagnostics code. Inside the command execution if this diagnostics code is present, then the player is going to take damage into his/her health asset. If the damage makes removes all the health then set the Victor of the game and set the game status.
        // - [OPTIONAL] Next coordinate can not be on top of the other player. But since the offline client does not know the location of the other player, this validation is not required.
    }
}