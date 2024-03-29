using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Commands;

namespace CaptainSonar.Common.Domain.Commands
{
    // Commands are requests from the client to the server
    // 1. @TODO: Put a diagnostics array into the response to the client if there is an error in the command list. Not every command can be registered since it might be invalid.
    // this diagnostics array will contain the error message and the command that caused the error.
    // 2. The system is always going to receive commands and provide the state of the game. At any point in time generateState(List<Command> commands) will return the
    // the associated state after validation.
    // 3. Command sequence is important. The server will validate the command sequence and if it is not in the correct order, it will return an error in the diagnostics array.
    // if an error is found, command excution will stop and valid state up to that point will be returned. Therefore diagnostics array will contain the error message and the command that caused the error.
    // IMPORTANT: Other player's location is not known to the player. Estimated location will be handled by the client ONLY.

    public abstract class Command<T>(CommandName name, T data) : ICommandBase where T : class
    {
        public CommandName Name { get; set; } = name;
        public T Data { get; set; } = data;
    }
}
