using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandInfoRemoveData
    {
        public required TeamName TeamName { get; init; }
        public required int Index { get; init; }
    }

    public class CommandInfoRemove(CommandInfoRemoveData data) : Command<CommandInfoRemoveData>(CommandName.Info_Remove, data)
    {
    }
}