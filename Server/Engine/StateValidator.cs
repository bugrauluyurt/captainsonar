using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Server.Engine
{
    public static class StateValidatorHelper
    {
        public static StateExecutionStep GenerateDiagnostics(StateExecutionStep stateExecutionStep, List<SystemException> exceptions)
        {
            List<StateDiagnostic> diagnosticsNext = stateExecutionStep.StateDiagnostics;
            for (int i = 0; i < exceptions.Count; i++)
            {
                diagnosticsNext.Add(new StateDiagnostic { Exception = exceptions[i] });
            }

            return stateExecutionStep with
            {
                StateDiagnostics = diagnosticsNext,
                HasDiagnosticsErrors = exceptions.Count > 0
            };
        }
    }

    public static class StateValidator
    {
        public static StateExecutionStep ValidateSessionStart(StateExecutionStep stateExecutionStep, Player player)
        {
            var state = stateExecutionStep.State;

            List<SystemException> exceptions = [];

            if (player is null)
            {
                exceptions.Add(new InvalidOperationException("Player is required to start the game"));
            }
            if (state.Victor is not null)
            {
                exceptions.Add(new InvalidOperationException("Game is already finished"));
            }
            if (state.Status != GameStatus.NotStarted)
            {
                exceptions.Add(new InvalidOperationException("Game is already started"));
            }
            if (state.TeamState[TeamName.Team1].Players.Count != 0 || state.TeamState[TeamName.Team2].Players.Count != 0)
            {
                exceptions.Add(new InvalidOperationException("Players are already added to the game. State is invalid."));
            }

            return StateValidatorHelper.GenerateDiagnostics(stateExecutionStep, exceptions);
        }

        public static StateExecutionStep ValidateSessionQuit(StateExecutionStep stateExecutionStep, Player player)
        {
            List<SystemException> exceptions = [];

            if (player is null)
            {
                exceptions.Add(new InvalidOperationException("Player is required to quit the game"));
            }

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

            return StateValidatorHelper.GenerateDiagnostics(stateExecutionStep, exceptions);
        }

        public static StateExecutionStep ValidateSessionEnd(StateExecutionStep stateExecutionStep, Player player)
        {
            List<SystemException> exceptions = [];

            if (player is null)
            {
                exceptions.Add(new InvalidOperationException("Player is required to end the game"));
            }

            if (stateExecutionStep.State.Victor is not null)
            {
                exceptions.Add(new InvalidOperationException("Game is already finished"));
            }

            if (stateExecutionStep.State.Status == GameStatus.NotStarted)
            {
                exceptions.Add(new InvalidOperationException("Game is not started yet"));
            }

            return StateValidatorHelper.GenerateDiagnostics(stateExecutionStep, exceptions);
        }

        public static StateExecutionStep ValidateSessionJoin(StateExecutionStep stateExecutionStep, Player player, TeamName teamName)
        {
            List<SystemException> exceptions = [];

            if (player is null)
            {
                exceptions.Add(new InvalidOperationException("Player is required to join the game"));
            }

            if (player is not null && stateExecutionStep.State.TeamState[teamName].Players.Contains(player))
            {
                exceptions.Add(new InvalidOperationException("Player is already in the team"));
            }

            return StateValidatorHelper.GenerateDiagnostics(stateExecutionStep, exceptions);
        }

        // ValidateMapMove(StateExecutionStep stateExecutionStep, TeamName teamName, Coordinate coordinate)
        // - Next coordinart can not be out of boundaries.
        // - Next coordinate can not be on top of an obstacle. The system should look at the obstacles and check if the next coordinate is an obstacle.
        // - Next coordinate can not belong to any of the player's dots. In other words the player can not move on top of his/her own path.
        // - If next coordinate is on top of a mine, then generate a diagnostics code. Inside the command execution if this diagnostics code is present, then the player is going to take damage into his/her health asset. If the damage makes removes all the health then set the Victor of the game and set the game status.
        // - [OPTIONAL] Next coordinate can not be on top of the other player. But since the offline client does not know the location of the other player, this validation is not required.

        // @TODO:
        // - GetMapObstacles => Write this method, all the dots with the obstacles should be returned in a list.
        // -
    }
}