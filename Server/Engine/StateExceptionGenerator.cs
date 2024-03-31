using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptainSonar.Server.Engine
{
    public static class StateDiagnosticsGenerator
    {

        public static StateExecutionStep Generate(
            StateExecutionStep stateExecutionStep,
            List<(bool, int)> conditionsForExceptions,
            List<(bool, int)> conditionsForInformatives)
        {
            // Exceptions block the state execution.
            List<SystemException> exceptions = [];
            foreach (var conditionForException in conditionsForExceptions)
            {
                if (!conditionForException.Item1)
                {
                    exceptions.Add(new InvalidOperationException(StateDiagnosticsCode.GetDiagnosticMessage(conditionForException.Item2)));
                }
            }

            // Informative diagnostics are not critical. They are just informative.
            List<(int, string)> informatives = [];
            foreach (var conditionForInformative in conditionsForInformatives)
            {
                if (!conditionForInformative.Item1)
                {
                    var diagnosticMessage = StateDiagnosticsCode.GetDiagnosticMessage(conditionForInformative.Item2);
                    informatives.Add((conditionForInformative.Item2, diagnosticMessage));
                }
            }

            return GenerateDiagnostics(stateExecutionStep, exceptions, informatives);
        }

        private static StateExecutionStep GenerateDiagnostics(
            StateExecutionStep stateExecutionStep,
            List<SystemException> exceptions,
            List<(int, string)> informatives
            )
        {
            List<StateDiagnostic> diagnosticsNext = stateExecutionStep.StateDiagnostics;
            for (int i = 0; i < exceptions.Count; i++)
            {
                diagnosticsNext.Add(new StateDiagnostic { Exception = exceptions[i] });
            }

            for (int i = 0; i < informatives.Count; i++)
            {
                diagnosticsNext.Add(new StateDiagnostic { DiagnosticMessage = informatives[i].Item2, DiagnosticsCode = informatives[i].Item1 });
            }

            return stateExecutionStep with
            {
                StateDiagnostics = diagnosticsNext,
                HasDiagnosticsErrors = exceptions.Count > 0
            };
        }
    }
}