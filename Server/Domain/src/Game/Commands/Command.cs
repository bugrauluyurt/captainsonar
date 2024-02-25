using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Game.Commands
{
    // Commands are requests from the client to the server
    // 1. @TODO: Put a diagnostics array into the response to the client if there is an error in the command list. Not every command can be registered since it might be invalid.
    // this diagnostics array will contain the error message and the command that caused the error.
    // 2. The system is always going to receive commands and provide the state of the game. At any point in time generateState(List<Command> commands) will return the
    // the associated state after validation.
    // 3. Command sequence is important. The server will validate the command sequence and if it is not in the correct order, it will return an error in the diagnostics array.
    // if an error is found, command excution will stop and valid state up to that point will be returned. Therefore diagnostics array will contain the error message and the command that caused the error.
    // IMPORTANT: Other player's location is not known to the player. Estimated location will be handled by the client ONLY.
    enum CommandName
    {
        Move, // Move self
        Surface,
        UseAsset,
        MarkAssetSlot, // Mark the asset slot from the asset board
        MarkRoomUnit, // Mark the room unit from the room board
        ReportSonar, // Report the sonar result to the other player (1 true, 1 false)
        ReportSection, // Report the section of the map to the other player (1 true, 1 false)
    }
    internal class Command<T>(T? data, CommandName name) where T : class
    {
        public required CommandName Name { get; set; } = name;
        public required T? Data { get; set; } = data;
    }
}
