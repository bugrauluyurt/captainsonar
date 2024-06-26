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

        [Fact]
        public void ValidateRoomUnitDamage_WithInvalidId_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var roomUnitPositionId = "Front:UP1";
            var validatedState = StateValidator.ValidateRoomUnitDamage(stateExecutionStep, roomUnitPositionId);
            var hasRoomUnitIdError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1013);
            Assert.False(hasRoomUnitIdError);

            var roomUnitPositionIdWithError = "Something:UP1";
            var validatedState2 = StateValidator.ValidateRoomUnitDamage(stateExecutionStep, roomUnitPositionIdWithError);
            var hasRoomUnitIdError2 = validatedState2.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1013);
            Assert.True(hasRoomUnitIdError2);
        }

        [Fact]
        public void ValidateRoomUnitsRepair_WithInvalidId_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var roomUnitPositionIds = new List<string>() { "Front:UP1", "FrontMiddle:UP2" };
            var validatedState = StateValidator.ValidateRoomUnitsRepair(stateExecutionStep, roomUnitPositionIds);
            var hasRoomUnitIdError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1013);
            Assert.False(hasRoomUnitIdError);

            var roomUnitPositionIdsWithError = new List<string>() { "Front:UP1", "Something:UP2" };
            var validatedState2 = StateValidator.ValidateRoomUnitsRepair(stateExecutionStep, roomUnitPositionIdsWithError);
            var hasRoomUnitIdError2 = validatedState2.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1013);
            Assert.True(hasRoomUnitIdError2);
        }

        [Fact]
        public void ValidateRoomUnitsByRepairType_WithValidRoomUnit_HasNoError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateRoomUnitsRepairByType(stateExecutionStep, TeamName.Team1, RoomUnitType.Orange);
            var hasRoomUnitTypeError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1014);
            Assert.False(hasRoomUnitTypeError);
        }

        [Fact]
        public void ValidateRoomUnitsByRepairType_WithUnlinkedRoomUnitType_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateRoomUnitsRepairByType(stateExecutionStep, TeamName.Team1, RoomUnitType.Unlinked);
            var hasRoomUnitTypeError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1015);
            Assert.True(hasRoomUnitTypeError);
        }

        [Fact]
        public void ValidateRoomUnitsByRepairType_WithAllUnitsDamaged_HasNoError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Vessel.DamageRoomUnitsByRoomUnitType(RoomUnitType.Orange);

            var validatedState = StateValidator.ValidateRoomUnitsRepairByType(stateExecutionStep, TeamName.Team1, RoomUnitType.Orange);
            var hasError = validatedState.StateDiagnosticsExceptions.Count > 0;
            Assert.False(hasError);
        }

        [Fact]
        public void ValidateRoomUnitsByRepairType_WithSomeUnitsDamaged_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Vessel.DamageRoomUnitsByRoomUnitType(RoomUnitType.Orange);
            RoomUnit? orangeRoomUnit = null;
            _ = stateExecutionStep.State.TeamState[TeamName.Team1].Vessel.Rooms.Any(room =>
            {
                // Find the room with Orange room units
                return room.GetRoomUnits().Any(roomUnit =>
                {
                    var isRoomUnitOrange = roomUnit.RoomUnitType == RoomUnitType.Orange;
                    if (isRoomUnitOrange)
                    {
                        orangeRoomUnit = roomUnit;
                    }
                    return isRoomUnitOrange;
                });
            });

            if (orangeRoomUnit is not null)
            {
                orangeRoomUnit.Repair();
            }

            var validatedState = StateValidator.ValidateRoomUnitsRepairByType(stateExecutionStep, TeamName.Team1, RoomUnitType.Orange);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1016);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetLoad_WithAllAssetsLoaded_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            foreach (var item in stateExecutionStep.State.TeamState[TeamName.Team1].Assets)
            {
                for (int i = 0; i < item.Slots.GetTotalSize(); i++)
                {
                    item.Slots.Load();
                }
            }
            var validatedState = StateValidator.ValidateAssetLoad(stateExecutionStep, TeamName.Team1, AssetName.Mine);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1018);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployMine_WithCoordinateOutOfBounds_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateAssetDeployMine(stateExecutionStep, TeamName.Team1, new Coordinate(0, 16));
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1008);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployMine_WithCoordinateOnPath_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var coordinate = new Coordinate(0, 2);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(coordinate));

            var validatedState = StateValidator.ValidateAssetDeployMine(stateExecutionStep, TeamName.Team1, coordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1019);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployMine_WithCoordinateOnObstacle_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var coordinate = new Coordinate(1, 2);

            var validatedState = StateValidator.ValidateAssetDeployMine(stateExecutionStep, TeamName.Team1, coordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1020);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployMine_WithEmptyMineSlots_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var coordinate = new Coordinate(0, 2);

            var validatedState = StateValidator.ValidateAssetDeployMine(stateExecutionStep, TeamName.Team1, coordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1021);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployMine_WithMineAlreadyDeployed_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var coordinate = new Coordinate(0, 2);
            stateExecutionStep.State.TeamState[TeamName.Team1].Mines.Add(new StateMine() { Dot = new Dot(coordinate) });

            var validatedState = StateValidator.ValidateAssetDeployMine(stateExecutionStep, TeamName.Team1, coordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1025);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployTorpedo_WithEmptyTorpedoSlots_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var coordinate = new Coordinate(0, 2);
            var pathCoordinate = new Coordinate(0, 1);

            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(pathCoordinate));

            var validatedState = StateValidator.ValidateAssetDeployTorpedo(stateExecutionStep, TeamName.Team1, coordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1021);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployTorpedo_WithInvalidDistance_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var pathCoordinate = new Coordinate(0, 2);
            var torpedoCoordinateInvalid = new Coordinate(4, 3);
            var torpedoCoordinateValid = new Coordinate(4, 2);
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(pathCoordinate));

            var validatedState = StateValidator.ValidateAssetDeployTorpedo(stateExecutionStep, TeamName.Team1, torpedoCoordinateInvalid);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1022);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployTorpedo_WithNoMoveYet_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var torpedoCoordinateValid = new Coordinate(4, 2);

            var validatedState = StateValidator.ValidateAssetDeployTorpedo(stateExecutionStep, TeamName.Team1, torpedoCoordinateValid);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1012);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeployDrone_WithEmptyDroneSlots_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateAssetDeployDrone(stateExecutionStep, TeamName.Team1);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1021);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeploySonar_WithEmptySlots_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);

            var validatedState = StateValidator.ValidateAssetDeploySonar(stateExecutionStep, TeamName.Team1);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1021);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeploySilence_WithEmptySlotsAndNoPlayerCoordinate_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            Coordinate userCoordinate = new(0, 0);
            List<Coordinate> coordinateListJumped = [new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(1, 2)];

            var validatedState = StateValidator.ValidateAssetDeploySilence(stateExecutionStep, TeamName.Team1, coordinateListJumped);
            var hasEmptyAssetError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1021);
            var hasPlayerNoCoordinateError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1012);
            Assert.True(hasEmptyAssetError);
            Assert.True(hasPlayerNoCoordinateError);
        }

        [Fact]
        public void ValidateAssetDeploySilence_WithFirstCoordinateIsNotAdjacent_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            Coordinate userCoordinate = new(0, 0);
            List<Coordinate> coordinateListJumped = [new Coordinate(0, 2), new Coordinate(0, 3), new Coordinate(0, 4)];
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(userCoordinate));

            var validatedState = StateValidator.ValidateAssetDeploySilence(stateExecutionStep, TeamName.Team1, coordinateListJumped);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1024);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDeploySilence_WithInvalidDistance_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            Coordinate userCoordinate = new(0, 0);
            List<Coordinate> coordinateListJumped = [new Coordinate(0, 2), new Coordinate(0, 3), new Coordinate(0, 5)];
            stateExecutionStep.State.TeamState[TeamName.Team1].Dots.Add(new Dot(userCoordinate));

            var validatedState = StateValidator.ValidateAssetDeploySilence(stateExecutionStep, TeamName.Team1, coordinateListJumped);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1022);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateAssetDetonateMine_WithNoMineOnCoordinate_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            Coordinate mineCoordinate = new(0, 1);

            var validatedState = StateValidator.ValidateAssetDetonateMine(stateExecutionStep, TeamName.Team1, mineCoordinate);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1026);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateInfoAdd_WithStringExceedingLimits_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            Coordinate infoCoordinate = new(0, 2);
            var text = "";
            for (int i = 0; i < 301; i++)
            {
                text += "a";
            }

            var validatedState = StateValidator.ValidateInfoAdd(stateExecutionStep, infoCoordinate, text);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1028);
            Assert.True(hasError);
        }

        [Fact]
        public void ValidateInfoRemove_WithInvalidIndex_ReturnsError()
        {
            var stateExecutionStep = TestUtils.CreateStateExecutionStep(null);
            stateExecutionStep.State.TeamState[TeamName.Team1].Info.Add(new StateInfo() { Coordinate = new Coordinate(0, 2), Text = "Test" });

            var validatedState = StateValidator.ValidateInfoRemove(stateExecutionStep, TeamName.Team1, 2);
            var hasError = validatedState.StateDiagnosticsExceptions.Any(x => x.DiagnosticCode == 1029);
            Assert.True(hasError);
        }
    }
}