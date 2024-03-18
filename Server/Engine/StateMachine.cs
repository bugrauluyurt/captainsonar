using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Utils;
using Common.Domain.Commands;

namespace CaptainSonar.Engine
{
    static class StateMachine
    {
        public static State HandleCommand(ICommandBase command, State state)
        {
            var _ = command switch
            {
                // @TODO: Write the correct commands here and handler for each command.
                // @TODO: Inside the handlers there should be validator.
                StartGameCommand startGameCommand => StartGame(startGameCommand.Player),
                _ => throw new InvalidOperationException("Invalid command")
            };

            return state;
        }

        public static State OnCommand(State state, ICommandBase command)
        {
            try
            {
                return HandleCommand(command, state);
            }
            catch (Exception e)
            {
                // @TODO: Log the error. There is an issue here. HandleCommand might mutate the state and leave it dirty.
                // then an error can happen and them a dirty state might be returned.
                return state;
            }
        }
    }
}
