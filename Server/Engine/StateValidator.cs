using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Common.Domain.Vessel;
using Common.Domain.Assets;

namespace CaptainSonar.Server.Engine
{
    public static class StateValidator
    {
        public static StateExecutionStep ValidateSessionStart(StateExecutionStep stateExecutionStep, Player player)
        {
            var state = stateExecutionStep.State;
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is null,
                    1001
                ),
                (
                    state.Victor is not null,
                    1002
                ),
                (
                    state.Status != GameStatus.NotStarted,
                    1003
                ),
                (
                    state.TeamState[TeamName.Team1].Players.Count != 0 || state.TeamState[TeamName.Team2].Players.Count != 0,
                    1005
                )
            ], []);
        }

        public static StateExecutionStep ValidateSessionQuit(StateExecutionStep stateExecutionStep, Player player)
        {
            // @INFO: Teams are allowed to have no players. At this point the session owner can send another invitation to another player.
            // @INFO: If the player is the owner of the session, this control is being done at session level not at state level.
            // if (player is not null)
            // {
            //     var team1Players = state.TeamState[TeamName.Team1].Players;
            //     var team2Players = state.TeamState[TeamName.Team2].Players;
            //     if ((!team1Players.Contains(player) && team1Players.Count == 1) || (!team2Players.Contains(player) && team1Players.Count == 1))
            //     {
            //         exceptions.Add(new InvalidOperationException("Player is the last player in the game"));
            //     }
            // }
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is null,
                    1001
                ),
            ], []);
        }

        public static StateExecutionStep ValidateSessionEnd(StateExecutionStep stateExecutionStep, Player player)
        {
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is null,
                    1001
                ),
                (
                    stateExecutionStep.State.Victor is not null,
                    1002
                ),
                (
                    stateExecutionStep.State.Status != GameStatus.NotStarted,
                    1004
                )
            ], []);
        }

        public static StateExecutionStep ValidateSessionJoin(StateExecutionStep stateExecutionStep, Player player, TeamName teamName)
        {
            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    player is null,
                    1001
                ),
                (
                    player is not null && !stateExecutionStep.State.TeamState[teamName].Players.Contains(player),
                    1007
                )
            ], []);
        }

        public static StateExecutionStep ValidateMapMove(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            Direction direction)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var lastKnownCoordinate = state.TeamState[teamName].Dots.Last().Location;
            var nextCoordinate = MapHelpers.GetNextCoordinateFromDirection(lastKnownCoordinate, direction);

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !MapHelpers.IsCoordinateInBounds(nextCoordinate, gridType),
                    1008
                ),
                (
                    MapHelpers.IsCoordinateOnObstacle(nextCoordinate, gridType),
                    1009
                ),
                (
                    MapHelpers.IsCoordinateOnPath(nextCoordinate, state.TeamState[teamName].Dots),
                    1010
                ),
                (
                    !MapHelpers.IsCoordinateAdjacent(nextCoordinate, state.TeamState[teamName].Dots.Last().Location),
                    1012
                ),
                (
                    !MapHelpers.CanMove(nextCoordinate, gridType, state.TeamState[teamName].Dots),
                    1011
                )
            ], []);
        }

        public static StateExecutionStep ValidateMapSurface(
            StateExecutionStep stateExecutionStep,
            TeamName teamName)
        {
            var state = stateExecutionStep.State;
            var dots = state.TeamState[teamName].Dots;
            var lastKnownDot = dots.Count == 0 ? null : dots.Last();

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    lastKnownDot is null,
                    1012
                ),
                (
                    !lastKnownDot?.Props.CanSurface ?? true,
                    1011
                )
            ], []);
        }

        public static StateExecutionStep ValidateRoomUnitDamage(
            StateExecutionStep stateExecutionStep,
            string roomUnitPositionId)
        {

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !RoomUnit.IsRoomUnitPositionIdValid(roomUnitPositionId),
                    1013
                ),
            ], []);
        }

        public static StateExecutionStep ValidateRoomUnitsRepair(
            StateExecutionStep stateExecutionStep,
            List<string> roomUnitPositionIds)
        {

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !roomUnitPositionIds.All(RoomUnit.IsRoomUnitPositionIdValid),
                    1013
                ),
            ], []);
        }

        public static StateExecutionStep ValidateRoomUnitsRepairByType(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            RoomUnitType roomUnitType)
        {
            var state = stateExecutionStep.State;
            var vessel = state.TeamState[teamName].Vessel;

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !Enum.IsDefined(typeof(RoomUnitType), roomUnitType),
                    1014
                ),
                (
                    roomUnitType == RoomUnitType.Unlinked,
                    1015
                ),
                (
                    !vessel.IsAllRoomUnitsWithRoomUnitTypeDamaged(roomUnitType),
                    1016
                )
            ], []);
        }

        public static StateExecutionStep ValidateAssetLoad(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            AssetName assetName)
        {
            var state = stateExecutionStep.State;
            var asset = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == assetName);

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    asset is null,
                    1017
                ),
                (
                    asset is not null && asset.Slots.IsLoaded,
                    1018
                )
            ], []);
        }

        public static StateExecutionStep ValidateAssetDeployMine(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            Coordinate coordinate)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var assetMine = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Mine);

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    !MapHelpers.IsCoordinateInBounds(coordinate, gridType),
                    1008
                ),
                (
                    MapHelpers.IsCoordinateOnPath(coordinate, state.TeamState[teamName].Dots),
                    1019
                ),
                (
                    MapHelpers.IsCoordinateOnObstacle(coordinate, gridType),
                    1020
                ),
                (
                    assetMine is not null && assetMine.Slots.IsEmpty,
                    1021
                ),
                (
                    state.TeamState[teamName].Mines.Any(mine => mine.Dot.Location.ToString() == coordinate.ToString()),
                    1025
                ),
            ], []);
        }

        public static StateExecutionStep ValidateAssetDeployTorpedo(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            Coordinate torpedoCoordinate)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var assetTorpedo = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Torpedo);
            var playerCoordinate = state.TeamState[teamName].Dots.Count == 0 ? null : state.TeamState[teamName].Dots.Last().Location;
            var dots = stateExecutionStep.State.Grid.GetDots();

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    playerCoordinate is null,
                    1012
                ),
                (
                    !MapHelpers.IsCoordinateInBounds(torpedoCoordinate, gridType),
                    1008
                ),
                (
                    MapHelpers.IsCoordinateOnPath(torpedoCoordinate, state.TeamState[teamName].Dots),
                    1019
                ),
                (
                    MapHelpers.IsCoordinateOnObstacle(torpedoCoordinate, gridType),
                    1020
                ),
                (
                    assetTorpedo is not null && assetTorpedo.Slots.IsEmpty,
                    1021
                ),
                (
                    playerCoordinate is not null && !MapHelpers.IsCoordinateWithinAllowedDistance(dots, playerCoordinate, torpedoCoordinate, Torpedo.MAX_RANGE),
                    1022
                )
            ], []);
        }

        public static StateExecutionStep ValidateAssetDeployDrone(
            StateExecutionStep stateExecutionStep,
            TeamName teamName)
        {
            var state = stateExecutionStep.State;
            var assetDrone = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Drone);


            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    assetDrone is not null && assetDrone.Slots.IsEmpty,
                    1021
                )
            ], []);
        }

        public static StateExecutionStep ValidateAssetDeploySonar(
            StateExecutionStep stateExecutionStep,
            TeamName teamName)
        {
            var state = stateExecutionStep.State;
            var assetSonar = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Sonar);

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [

                (
                    assetSonar is not null && assetSonar.Slots.IsEmpty,
                    1021
                )
            ], []);
        }

        public static StateExecutionStep ValidateAssetDeploySilence(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            List<Coordinate> coordinatesJumped)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var assetSilence = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Silence);
            var playerDots = state.TeamState[teamName].Dots;
            var playerLastCoordinate = state.TeamState[teamName].Dots.Count == 0 ? null : state.TeamState[teamName].Dots.Last().Location;
            var coordinatesJumpedLastCoordinate = coordinatesJumped.Last();
            var dots = stateExecutionStep.State.Grid.GetDots();

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    playerLastCoordinate is null,
                    1012
                ),
                (
                    assetSilence is not null && assetSilence.Slots.IsEmpty,
                    1021
                ),
                (
                    playerLastCoordinate is not null && !MapHelpers.IsCoordinateWithinAllowedDistance(dots, playerLastCoordinate, coordinatesJumpedLastCoordinate, Silence.MAX_RANGE),
                    1022
                ),
                (
                    playerLastCoordinate is not null && !MapHelpers.IsCoordinateAdjacent(playerLastCoordinate, coordinatesJumped.First()),
                    1024
                ),
                (
                    !MapHelpers.IsCoordinateListAdjacent(coordinatesJumped),
                    1030
                ),
                (
                    !MapHelpers.IsCoordinateListValid(coordinatesJumped, gridType, playerDots),
                    1023
                ),
            ], []);
        }

        public static StateExecutionStep ValidateAssetDetonateMine(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            Coordinate mineCoordinate)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var assetMine = state.TeamState[teamName].Assets.FirstOrDefault(asset => asset.AssetName == AssetName.Mine);
            var mine = state.TeamState[teamName].Mines.FirstOrDefault(mine => mine.Dot.Location.ToString() == mineCoordinate.ToString());

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    mine is null,
                    1026
                ),
                (
                    assetMine is not null && assetMine.Slots.IsEmpty,
                    1021
                ),
                (
                    !MapHelpers.IsCoordinateInBounds(mineCoordinate, gridType),
                    1008
                ),
                (
                    MapHelpers.IsCoordinateOnObstacle(mineCoordinate, gridType),
                    1020
                ),
            ], []);
        }

        public static StateExecutionStep ValidateInfoAdd(
            StateExecutionStep stateExecutionStep,
            Coordinate? location,
            string? text)
        {
            var state = stateExecutionStep.State;
            var grid = state.Grid;
            var gridType = grid.MapType;
            var textGenerated = text ?? "";

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    location is null && string.IsNullOrEmpty(textGenerated),
                    1027
                ),
                (
                    location is not null && !MapHelpers.IsCoordinateInBounds(location, gridType),
                    1008
                ),
                (
                    !string.IsNullOrEmpty(textGenerated) && textGenerated.Length > 300,
                    1028
                )
            ], []);
        }

        public static StateExecutionStep ValidateInfoRemove(
            StateExecutionStep stateExecutionStep,
            TeamName teamName,
            int index)
        {
            var state = stateExecutionStep.State;
            var infoList = state.TeamState[teamName].Info;

            return StateDiagnosticsGenerator.Generate(stateExecutionStep, [
                (
                    infoList[index] is null || index < 0 || index >= infoList.Count,
                    1029
                ),
            ], []);
        }



        // ValidateMapMove(StateExecutionStep stateExecutionStep, TeamName teamName, Coordinate coordinate)
        // - Next coordinart can not be out of boundaries.
        // - Next coordinate can not be on top of an obstacle. The system should look at the obstacles and check if the next coordinate is an obstacle.
        // - Next coordinate can not belong to any of the player's dots. In other words the player can not move on top of his/her own path.
        // - If next coordinate is on top of a mine, then generate a diagnostics code. Inside the command execution if this diagnostics code is present, then the player is going to take damage into his/her health asset. If the damage makes removes all the health then set the Victor of the game and set the game status.
        // - [OPTIONAL] Next coordinate can not be on top of the other player. But since the offline client does not know the location of the other player, this validation is not required.
    }
}