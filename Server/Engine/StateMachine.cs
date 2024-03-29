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

namespace CaptainSonar.Server.Engine
{
    public enum DiagnosticsCode
    {
        DamageRecieved = 0
    };

    public class StateDiagnostic
    {
        public SystemException? Exception { get; set; }
        public string? DiagnosticMessage { get; set; }
        public int? DiagnosticsCode { get; set; }
    }

    // Every execution returns a new state step
    public record StateExecutionStep
    {
        public State State { get; set; } = null!; // Current state
        public List<ICommandBase> Commands { get; init; } = []; // Existing previous commands. Pipe an empty list if new.
        public List<StateDiagnostic> StateDiagnostics { get; init; } = []; // Existing diagnostics. Pipe an empty list if new.
        public bool HasDiagnosticsErrors { get; init; } = false; // Means that the state has been violated. Game is not valid.
    }

    public static class StateMachineHelper
    {
        public static StateExecutionStep ExecStep(
            Func<StateExecutionStep> validatorFn,
            Func<StateExecutionStep> executorFn
        )
        {
            StateExecutionStep validatedStep = validatorFn();
            if (validatedStep.HasDiagnosticsErrors)
            {
                return validatedStep;
            }

            return executorFn();
        }

        public static StateExecutionStep ComposeState(
            StateExecutionStep stepExecutionStep,
            List<Func<State, State>> composeFunctions
        )
        {
            State stateNext = stepExecutionStep.State;
            foreach (var composeFunction in composeFunctions)
            {
                stateNext = composeFunction(stateNext);
            }

            return stepExecutionStep with { State = stateNext };
        }
    }

    public static class StateMachine
    {
        public static StateExecutionStep ExecSessionStart(
            StateExecutionStep stateExecutionStep,
            CommandSessionStart command)
        {
            var player = command.Data.Player;

            return StateMachineHelper.ExecStep(
                // Validator
                () => StateValidator.ValidateSessionStart(stateExecutionStep, player),
                // Executor
                () =>
                {
                    return StateMachineHelper.ComposeState(
                        stateExecutionStep,
                        [
                            (stateNext) => StateHelper.AddPlayerToTeam(stateNext, TeamName.Team1, player),
                            (stateNext) => StateHelper.StartGame(stateExecutionStep.State)
                        ]
                    );
                }
            );
        }

        public static StateExecutionStep ExecSessionEnd(
            StateExecutionStep stateExecutionStep,
            CommandSessionEnd command)
        {
            var player = command.Data.Player;

            return StateMachineHelper.ExecStep(
                // Validator
                () => StateValidator.ValidateSessionEnd(stateExecutionStep, player),
                // Executor
                () =>
                {
                    return StateMachineHelper.ComposeState(
                        stateExecutionStep,
                        [
                            (stateNext) => StateHelper.RemovePlayerFromGame(stateNext, player),
                        ]
                    );
                }
            );
        }

        public static StateExecutionStep ExecSessionQuit(
            StateExecutionStep stateExecutionStep,
            CommandSessionQuit command)
        {
            var player = command.Data.Player;

            return StateMachineHelper.ExecStep(
                // Validator
                () => StateValidator.ValidateSessionQuit(stateExecutionStep, player),
                // Executor
                () =>
                {
                    return StateMachineHelper.ComposeState(
                        stateExecutionStep,
                        [
                            (stateNext) => StateHelper.RemovePlayerFromGame(stateNext, player),
                        ]
                    );
                }
            );
        }

        public static StateExecutionStep ExecSessionJoin(
            StateExecutionStep stateExecutionStep,
            CommandSessionJoin command)
        {
            var player = command.Data.Player;
            var teamName = command.Data.TeamName;

            return StateMachineHelper.ExecStep(
                // Validator
                () => StateValidator.ValidateSessionJoin(stateExecutionStep, player, teamName),
                // Executor
                () =>
                {
                    return StateMachineHelper.ComposeState(
                        stateExecutionStep,
                        [
                            (stateNext) => StateHelper.AddPlayerToTeam(stateNext, teamName, player),
                        ]
                    );
                }
            );
        }

        // @TODO: Complete the other commands

        // ExecCommand takes a command and executes it on the current state while updates the state and the commands list.
        public static StateExecutionStep ExecCommand(
            ICommandBase command, // New command
            StateExecutionStep stateExecutionStep)
        {
            StateExecutionStep stateExecutionStepNext = command switch
            {
                CommandSessionStart commandStartGame => ExecSessionStart(stateExecutionStep, commandStartGame),
                CommandSessionEnd commandEndGame => ExecSessionEnd(stateExecutionStep, commandEndGame),
                CommandSessionQuit commandQuitGame => ExecSessionQuit(stateExecutionStep, commandQuitGame),
                CommandSessionJoin commandSessionJoin => ExecSessionJoin(stateExecutionStep, commandSessionJoin),
                _ => throw new InvalidOperationException("Invalid command")
            };

            return stateExecutionStepNext;
        }

        // @TODO: Either inside this method or within the higher level functions we need to validate the given order of commands. Order might be faulty.
        // @TODO: The order should determine the turn of the game.
        // Write a method called ExecCommands => This should take a list commands and generate a new state. Returna a StateExecutionStep.
    }
}
