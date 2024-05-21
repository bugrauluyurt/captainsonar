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
using CaptainSonar.Common.Domain.Vessel;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Tests.Server.Engine
{
    public class StateMachineTest
    {
        [Fact]
        public void ExecSessionStart_WithPlayer_AddsPlayerToTeam()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandSessionStart = new CommandSessionStart(new CommandSessionStartData() { Player = new Player() { Name = "Player 1", Id = "1" } });

            var nextState = StateMachine.ExecCommand(commandSessionStart, stateExecutionStep);
            Assert.Single(nextState.State.TeamState[TeamName.Team1].Players);
            Assert.Equal("Player 1", nextState.State.TeamState[TeamName.Team1].Players[0].Name);
        }

        [Fact]
        public void ExecSessionStart_WithPlayer_StartsTheSession()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandSessionStart = new CommandSessionStart(new CommandSessionStartData() { Player = new Player() { Name = "Player 1", Id = "1" } });

            var nextState = StateMachine.ExecCommand(commandSessionStart, stateExecutionStep);
            Assert.Equal(GameStatus.InProgress, nextState.State.Status);
            Assert.Equal(TeamName.Team1, nextState.State.Turn?.Team);
        }

        [Fact]
        public void ExecSessionEnd_WithPlayer_RemovesPlayerFromGame()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var player = new Player() { Name = "Player 1", Id = "1" };
            stateExecutionStep.State.TeamState[TeamName.Team1].Players.Add(player);
            var commandSessionEnd = new CommandSessionEnd(new CommandSessionEndData() { Player = player });

            var nextState = StateMachine.ExecCommand(commandSessionEnd, stateExecutionStep);
            var isPlayerInTeam = nextState.State.TeamState[TeamName.Team1].Players.Any(p => p.Id == "1");
            Assert.False(isPlayerInTeam);
        }

        [Fact]
        public void ExecSessionQuit_WithPlayer_RemovesPlayerFromGame()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var player = new Player() { Name = "Player 1", Id = "1" };
            stateExecutionStep.State.TeamState[TeamName.Team1].Players.Add(player);
            var commandSessionQuit = new CommandSessionQuit(new CommandSessionQuitData() { Player = player });

            var nextState = StateMachine.ExecCommand(commandSessionQuit, stateExecutionStep);
            var isPlayerInTeam = nextState.State.TeamState[TeamName.Team1].Players.Any(p => p.Id == "1");
            Assert.False(isPlayerInTeam);
        }


        [Fact]
        public void ExecMapMove_WithValidDirection_MovesToTeamToDirection()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var playerLastCoordinate = new Coordinate(1, 1);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(playerLastCoordinate));
            var commandMapMove = new CommandMapMove(new CommandMapMoveData() { TeamName = TeamName.Team1, Direction = Direction.North });

            var nextState = StateMachine.ExecCommand(commandMapMove, stateExecutionStep);
            var lastDot = nextState.State.TeamState[TeamName.Team1].Dots.Last().ToString();
            Assert.Equal("0:1", lastDot);
        }

        [Fact]
        public void ExecMapSurface_WithValidDots_RoomsRepairedDotCleared()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var playerLastCoordinate = new Coordinate(1, 1);
            var positionId = "Front:UP1";
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(playerLastCoordinate));

            stateExecutionStep.State.TeamState[TeamName.Team1].Vessel.DamageRoomUnitByPositionId(positionId);

            var commandMapSurface = new CommandMapSurface(new CommandMapSurfaceData() { TeamName = TeamName.Team1 });

            var nextState = StateMachine.ExecCommand(commandMapSurface, stateExecutionStep);
            var isDotsEmpty = nextState.State.TeamState[TeamName.Team1].Dots.Count == 0;
            var isRoomDamaged = nextState.State.TeamState[TeamName.Team1].Vessel.FindRoomRoomByPosition(RoomPosition.Front).GetRoomUnits().Any(roomUnit => roomUnit.PositionId == positionId && roomUnit.IsDamaged());

            Assert.False(isRoomDamaged);
            Assert.True(isDotsEmpty);
        }

        [Fact]
        public void ExecRoomUnitDamage_WithRoomPositionId_RoomsIsDamaged()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var positionId = "Front:UP1";

            var commandRoomUnitDamage = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId });

            var nextState = StateMachine.ExecCommand(commandRoomUnitDamage, stateExecutionStep);
            var isRoomDamaged = nextState.State.TeamState[TeamName.Team1].Vessel.FindRoomRoomByPosition(RoomPosition.Front).GetRoomUnits().Any(roomUnit => roomUnit.PositionId == positionId && roomUnit.IsDamaged());

            Assert.True(isRoomDamaged);
        }

        [Fact]
        public void ExecRoomUnitRepair_WithRoomPositionId_RoomsIsRepaired()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var positionId = "Front:UP1";

            var commandRoomUnitDamage = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId });

            var nextState = StateMachine.ExecCommand(commandRoomUnitDamage, stateExecutionStep);
            var isRoomDamaged = nextState.State.TeamState[TeamName.Team1].Vessel.IsRoomUnitDamaged(positionId);

            Assert.True(isRoomDamaged);

            var commandRoomUnitRepair = new CommandRoomUnitsRepair(new CommandRoomUnitsRepairData() { TeamName = TeamName.Team1, RoomUnitPositionIds = [positionId] });
            var nextState2 = StateMachine.ExecCommand(commandRoomUnitRepair, stateExecutionStep);

            var isRoomRepaired = !nextState2.State.TeamState[TeamName.Team1].Vessel.IsRoomUnitDamaged(positionId);

            Assert.True(isRoomRepaired);
        }

        [Fact]
        public void ExecRoomUnitRepair_WithRoomPositionId_RoomUnitRepairedByType()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var positionId1 = "Front:UP1";
            var positionId2 = "Front:UP2";
            var positionId3 = "Front:UP3";
            var positionId4 = "Rear:UP3";

            var commandRoomUnitDamage1 = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId1 });
            var commandRoomUnitDamage2 = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId2 });
            var commandRoomUnitDamage3 = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId3 });
            var commandRoomUnitDamage4 = new CommandRoomUnitDamage(new CommandRoomUnitDamageData() { TeamName = TeamName.Team1, RoomUnitPositionId = positionId4 });

            var nextState1 = StateMachine.ExecCommand(commandRoomUnitDamage1, stateExecutionStep);
            var nextState2 = StateMachine.ExecCommand(commandRoomUnitDamage2, nextState1);
            var nextState3 = StateMachine.ExecCommand(commandRoomUnitDamage3, nextState2);
            var nextState4 = StateMachine.ExecCommand(commandRoomUnitDamage4, nextState3);

            var commandRoomUnitRepairByType = new CommandRoomUnitsRepairByType(new CommandRoomUnitsRepairByTypeData() { TeamName = TeamName.Team1, RoomUnitType = RoomUnitType.Yellow });

            StateMachine.ExecCommand(commandRoomUnitRepairByType, nextState4);

            var isRoomRepaired = !nextState2.State.TeamState[TeamName.Team1].Vessel.IsRoomUnitDamaged(positionId1);

            Assert.True(isRoomRepaired);
        }

        [Fact]
        public void ExecAssetLoad_WithAssetName_AssetIsLoaded()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var commandAssetLoad = new CommandAssetIncrease(new CommandAssetIncreaseData() { TeamName = TeamName.Team1, AssetName = AssetName.Mine });

            var nextState = StateMachine.ExecCommand(commandAssetLoad, stateExecutionStep);
            var assetMine = nextState.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Mine);
            var isAssetLoadedOne = assetMine is not null && assetMine.Slots.GetCurrentSize() == 1;
            Assert.True(isAssetLoadedOne);
        }

        [Fact]
        public void ExecAssetDeployMine_WithValidCoordinate_MineIsDeployed()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var commandAssetDeployMine = new CommandAssetDeployMine(new CommandAssetDeployMineData() { TeamName = TeamName.Team1, Coordinate = new Coordinate(1, 1) });
            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Mine);

            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var nextState = StateMachine.ExecCommand(commandAssetDeployMine, stateExecutionStep);
            var hasMine = nextState.State.TeamState[TeamName.Team1].Mines.Count == 1;
            var isMineAssetEmptied = asset?.Slots.GetCurrentSize() == 0;

            Assert.True(isMineAssetEmptied);
            Assert.True(hasMine);
        }

        [Fact]
        public void ExecAssetDeployTorpedo_WithValidCoordinate_TorpedoGetsDeployed()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 1)));
            var commandAssetDeployTorpedo = new CommandAssetDeployTorpedo(new CommandAssetDeployTorpedoData() { TeamName = TeamName.Team1, Coordinate = new Coordinate(0, 3) });
            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Torpedo);

            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var nextState = StateMachine.ExecCommand(commandAssetDeployTorpedo, stateExecutionStep);
            var isTorpedoEmptied = asset?.Slots.GetCurrentSize() == 0;

            Assert.True(isTorpedoEmptied);
        }

        [Fact]
        public void ExecAssetDeployDrone_WithTeamName_DroneGetsDeployed()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 1)));
            var commandAssetDeployDrone = new CommandAssetDeployDrone(new CommandAssetDeployDroneData() { TeamName = TeamName.Team1 });
            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Drone);

            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var nextState = StateMachine.ExecCommand(commandAssetDeployDrone, stateExecutionStep);
            var isDroneEmptied = asset?.Slots.GetCurrentSize() == 0;

            Assert.True(isDroneEmptied);
        }

        [Fact]
        public void ExecAssetDeploySonar_WithTeamName_SonarGetsDeployed()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            var commandAssetDeploySonar = new CommandAssetDeploySonar(new CommandAssetDeploySonarData() { TeamName = TeamName.Team1 });
            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Sonar);

            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var nextState = StateMachine.ExecCommand(commandAssetDeploySonar, stateExecutionStep);
            var isSonarEmptied = asset?.Slots.GetCurrentSize() == 0;

            Assert.True(isSonarEmptied);
        }

        [Fact]
        public void ExecAssetDeploySilence_WithCoordinates_PlayerJumpsCoordinates()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 1)));
            var commandAssetDeploySilence = new CommandAssetDeploySilence(new CommandAssetDeploySilenceData() { TeamName = TeamName.Team1, Coordinates = [new Coordinate(0, 2), new Coordinate(0, 3), new Coordinate(0, 4)] });
            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Silence);

            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var nextState = StateMachine.ExecCommand(commandAssetDeploySilence, stateExecutionStep);
            var isSilenceEmptied = asset?.Slots.GetCurrentSize() == 0;
            var isPlayerJumped = nextState.State.TeamState[TeamName.Team1].Dots.Count == 4;

            Assert.True(isSilenceEmptied);
            Assert.True(isPlayerJumped);
        }

        [Fact]
        public void ExecAssetDetonateMine_WithCoordinates_MineGetsDetonated()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(new Coordinate(0, 1)));
            stateExecutionStep.State.TeamState[TeamName.Team1].Mines.Add(new StateMine() { Dot = new Dot(new Coordinate(0, 4)) });

            var asset = stateExecutionStep.State.TeamState[TeamName.Team1].Assets.ToList().FirstOrDefault(asset => asset.AssetName == AssetName.Mine);
            for (int i = 0; i < asset?.Slots.GetTotalSize(); i++)
            {
                asset.Slots.Load();
            }

            var commandAssetDetonateMine = new CommandAssetDetonateMine(new CommandAssetDetonateMineData() { TeamName = TeamName.Team1, Coordinate = new Coordinate(0, 4) });

            var nextState = StateMachine.ExecCommand(commandAssetDetonateMine, stateExecutionStep);

            var isMineDetonated = !nextState.State.TeamState[TeamName.Team1].Mines.Any(mine => mine.Dot.Location.Row == 0 && mine.Dot.Location.Column == 4);

            Assert.True(isMineDetonated);
        }

        [Fact]
        public void ExecInfoUpsert_WithNewTextAndLocation_InfoGetsAdded()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var commandInfoUpsert = new CommandInfoUpsert(new CommandInfoUpsertData() { TeamName = TeamName.Team1, Coordinate = null, Text = "Hello World", Index = null });

            var nextState = StateMachine.ExecCommand(commandInfoUpsert, stateExecutionStep);

            var isInfoAdded = nextState.State.TeamState[TeamName.Team1].Info.Any(info => info.Text == "Hello World" && info.Coordinate is null);
            Assert.True(isInfoAdded);
        }

        [Fact]
        public void ExecInfoUpsert_WithTextLocationAndIndex_InfoGetsUpserted()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var commandInfoUpsert = new CommandInfoUpsert(new CommandInfoUpsertData() { TeamName = TeamName.Team1, Coordinate = null, Text = "Hello World", Index = null });

            var nextState = StateMachine.ExecCommand(commandInfoUpsert, stateExecutionStep);

            var commandInfoUpsertWithIndex = new CommandInfoUpsert(new CommandInfoUpsertData() { TeamName = TeamName.Team1, Coordinate = null, Text = "Hello Neptune", Index = 0 });
            var nextStateNew = StateMachine.ExecCommand(commandInfoUpsertWithIndex, nextState);

            var isInfoUpserted = nextStateNew.State.TeamState[TeamName.Team1].Info.Any(info => info.Text == "Hello Neptune" && info.Coordinate is null) && nextStateNew.State.TeamState[TeamName.Team1].Info.Count == 1;

            Assert.True(isInfoUpserted);
        }

        [Fact]
        public void ExecInfoRemove_WithIndex_InfoGetsRemoved()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var commandInfoUpsert = new CommandInfoUpsert(new CommandInfoUpsertData() { TeamName = TeamName.Team1, Coordinate = null, Text = "Hello World", Index = null });

            var nextState = StateMachine.ExecCommand(commandInfoUpsert, stateExecutionStep);

            var commandInfoUpsertWithIndex = new CommandInfoUpsert(new CommandInfoUpsertData() { TeamName = TeamName.Team1, Coordinate = null, Text = "Hello Neptune", Index = null });
            var nextStateNew = StateMachine.ExecCommand(commandInfoUpsertWithIndex, nextState);

            var commandInfoRemove = new CommandInfoRemove(new CommandInfoRemoveData() { Index = 0, TeamName = TeamName.Team1 });
            var nextStateWithRemoved = StateMachine.ExecCommand(commandInfoRemove, nextStateNew);

            var isInfoRemoved = !nextStateWithRemoved.State.TeamState[TeamName.Team1].Info.Any(info => info.Text == "Hello World" && info.Coordinate is null) && nextStateWithRemoved.State.TeamState[TeamName.Team1].Info.Count == 1;
            Assert.True(isInfoRemoved);
        }
    }
}