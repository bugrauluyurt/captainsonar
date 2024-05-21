using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Map;
using Common.Domain.Commands;

namespace CaptainSonar.Server.Engine
{
    public class StateEngine
    {
        public required StateExecutionStep StateExecutionStep { get; set; }
        public required GridType GridType { get; set; }

        public StateEngine(GridType gridType)
        {
            GridType = gridType;
            Init(gridType);
        }

        public void Init(GridType gridType)
        {
            var grid = new Grid(gridType);
            StateExecutionStep = new StateExecutionStep()
            {
                State = StateHelper.CreateState(grid),
                Commands = []
            };
        }

        public void Reset()
        {
            Init(GridType);
        }

        public void ProcessCommand(ICommandBase command)
        {
            try
            {
                StateExecutionStep = StateMachine.ExecCommand(command, StateExecutionStep);
            }
            catch (Exception e)
            {
                var stateDiagnostic = new StateDiagnostic()
                {
                    DiagnosticMessage = e.Message,
                    DiagnosticCode = 1000,
                    StackTrace = e.StackTrace
                };
                StateExecutionStep.StateDiagnosticsExceptions.Add(stateDiagnostic);
            }
        }

        public void ProcessCommands(ICommandBase[] commands)
        {
            foreach (var command in commands)
            {
                ProcessCommand(command);
                if (StateExecutionStep.HasDiagnosticsErrors)
                {
                    // @INFO: If there are any exceptions, stop executing the commands.
                    return;
                }
            }
        }
    }
}