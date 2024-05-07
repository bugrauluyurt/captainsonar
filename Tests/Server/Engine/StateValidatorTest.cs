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

namespace CaptainSonar.Tests.Server.Engine
{
    public class StateValidatorTest
    {
        [Fact]
        public void ValidateSessionStart_WithNullPlayer_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateSessionStart(stateExecutionStep, player: null);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1001);
            Assert.True(isDiagnosticsGenerated);
        }

        [Fact]
        public void ValidateSessionStart_WithVictor_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.Victor = TeamName.Team1;

            var validatedState = StateValidator.ValidateSessionStart(stateExecutionStep, player: null);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1002);
            Assert.True(isDiagnosticsGenerated);
        }

        [Fact]
        public void ValidateSessionQuit_WithNullPlayer_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateSessionQuit(stateExecutionStep, player: null);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1001);
            Assert.True(isDiagnosticsGenerated);
        }
    }
}