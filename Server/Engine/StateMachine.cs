using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Utils;
using CaptainSonar.Server.Engine;
using Common.Domain.Commands;

namespace CaptainSonar.Engine
{
    public class StateDiagnostic
    {
        public SystemException? Exception { get; set; }
        public string? DiagnosticMessage { get; set; }
    }

    // Every execution returns a new state step
    public record StateExecutionStep
    {
        public State State { get; set; } = null!; // Current state
        public List<ICommandBase> Commands { get; init; } = []; // Existing previous commands. Pipe an empty list if new.
        public List<StateDiagnostic> StateDiagnostics { get; init; } = []; // Existing diagnostics. Pipe an empty list if new.
        public bool HasDiagnosticsErrors { get; init; } = false; // Means that the state has been violated. Game is not valid.
    }

    static class StateMachine
    {
        public static StateExecutionStep ExecSessionStart(
            Player player,
            StateExecutionStep stateExecutionStep)
        {

            // Validation
            StateExecutionStep stepNext = StateValidator.ValidateSessionStart(stateExecutionStep, player);

            if (stepNext.HasDiagnosticsErrors)
            {
                return stepNext;
            }
            // Execution
            stepNext.State = StateHelper.AddPlayerToTeam(stepNext.State, TeamName.Team1, player);
            stepNext.State = StateHelper.StartGame(stepNext.State);


            return stepNext;
        }

        public static StateExecutionStep ExecSessionEnd(
            Player player,
            StateExecutionStep stateExecutionStep)
        {
            // Validation
            StateExecutionStep stepNext = StateValidator.ValidateSessionQuit(stateExecutionStep, player);
            if (stepNext.HasDiagnosticsErrors)
            {
                return stepNext;
            }

            // Execution
            stepNext.State = StateHelper.RemovePlayerFromGame(stepNext.State, player);

            return stepNext;
        }

        // ExecCommand takes a command and executes it on the current state while updates the state and the commands list.
        public static StateExecutionStep ExecCommand(
            ICommandBase command, // New command
            StateExecutionStep stateExecutionStep)
        {
            StateExecutionStep stateExecutionStepNext = command switch
            {
                CommandSessionStart commandStartGame => ExecSessionStart(commandStartGame.Data!.Player, stateExecutionStep),
                _ => throw new InvalidOperationException("Invalid command")
            };

            return stateExecutionStepNext;
        }

        // @TODO: Either inside this method or within the higher level functions we need to validate the given order of commands. Order might be faulty.
        // @TODO: The order should determine the turn of the game.
        // Write a method called ExecCommands => This should take a list commands and generate a new state. Returna a StateExecutionStep.
    }
}
