using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using CaptainSonar.Server.Engine;
using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Tests.Utils;
using Dumpify;

namespace CaptainSonar.Tests.Server.Engine
{
    public class StateMachineTest
    {

        [Fact]
        public void ExecSessionStart_WithNullPlayer_ReturnsError()
        {
            var StateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateSessionStart(StateExecutionStep, null);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1001);
            Assert.True(isDiagnosticsGenerated);
        }
    }
}