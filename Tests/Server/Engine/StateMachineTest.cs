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
    public class StateMachineTest
    {
        [Fact]
        public void ValidateSessionStart_WithPlayer_AddsPlayerToTeam()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandSessionStart = new CommandSessionStart(new CommandSessionStartData() { Player = new Player() { Name = "Player 1", Id = "1" } });

            var nextState = StateMachine.ExecCommand(commandSessionStart, stateExecutionStep);
            Assert.Single(nextState.State.TeamState[TeamName.Team1].Players);
            Assert.Equal("Player 1", nextState.State.TeamState[TeamName.Team1].Players[0].Name);
        }

        [Fact]
        public void ValidateSessionStart_WithPlayer_StartsTheSession()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandSessionStart = new CommandSessionStart(new CommandSessionStartData() { Player = new Player() { Name = "Player 1", Id = "1" } });

            var nextState = StateMachine.ExecCommand(commandSessionStart, stateExecutionStep);
            Assert.Equal(GameStatus.InProgress, nextState.State.Status);
            Assert.Equal(TeamName.Team1, nextState.State.Turn?.Team);
        }
    }
}