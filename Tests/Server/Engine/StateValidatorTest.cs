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

        [Fact]
        public void ValidateMapMove_WithOutOfBoundsCoordinates_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 0), new DotProps() { HasObstacle = false, CanSurface = true }));

            var validatedState = StateValidator.ValidateMapMove(stateExecutionStep, TeamName.Team1, Direction.North);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1008);
            Assert.True(isDiagnosticsGenerated);
        }

        [Fact]
        public void ValidateMapMove_WithCoordinateOnObstacle_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 2)));

            var validatedState = StateValidator.ValidateMapMove(stateExecutionStep, TeamName.Team1, Direction.South);
            var isDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1009);
            Assert.True(isDiagnosticsGenerated);
        }

        [Fact]
        public void ValidateMapMove_WithCoordinateOnPlayerPath_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots
                .AddRange(new Dot[] { new(new Coordinate(0, 0)), new(new Coordinate(0, 1)), new(new Coordinate(1, 1)) });

            var validatedState = StateValidator.ValidateMapMove(stateExecutionStep, TeamName.Team1, Direction.North);
            var isPathErrorDiagnosticsGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1010);
            var isMoveErrorDiagnosticsGenerated2 = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1011);
            Assert.True(isPathErrorDiagnosticsGenerated);
            Assert.True(isMoveErrorDiagnosticsGenerated2);
        }

        [Fact]
        public void ValidateMapSurface_WithNoDots_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateMapSurface(stateExecutionStep, TeamName.Team1);
            var isNoLocationErrorGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1012);
            Assert.True(isNoLocationErrorGenerated);
        }

        [Fact]
        public void ValidateMapSurface_WithLastDotThatCanNotSurface_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 2), new DotProps() { CanSurface = false }));

            var validatedState = StateValidator.ValidateMapSurface(stateExecutionStep, TeamName.Team1);
            var isCanNotSurfaceErrorGenerated = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1011);
            Assert.True(isCanNotSurfaceErrorGenerated);
        }
    }
}