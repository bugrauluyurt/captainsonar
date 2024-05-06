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
        public void ValidateSessionStartt_WithNullPlayer_AddsPlayerToTeam()
        {
            var StateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandSessionStart = new CommandSessionStart(new CommandSessionStartData() { Player = new Player() { Name = "Player 1", Id = "1" } });
            Assert.Single(StateMachine.ExecCommand(commandSessionStart, StateExecutionStep).State.TeamState[TeamName.Team1].Players);
            Assert.Equal("Player 1", StateExecutionStep.State.TeamState[TeamName.Team1].Players[0].Name);
        }
    }
}