using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Engine;

namespace CaptainSonar.Server.Engine
{
    public static class StateValidator
    {
        public static StateExecutionStep ValidateStartGame(StateExecutionStep stateExecutionStep, Player player)
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
}