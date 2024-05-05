using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandInfoRemoveData
    {
        public TeamName TeamName { get; set; }
        public int Index { get; set; }
    }

    public class CommandInfoRemove(CommandInfoRemoveData data) : Command<CommandInfoRemoveData>(CommandName.Info_Remove, data)
    {
    }
}