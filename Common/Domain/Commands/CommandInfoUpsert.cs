using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandInfoUpsertData
    {
        public TeamName TeamName { get; set; }
        public string Text { get; set; } = null!;
        public Coordinate Location { get; set; } = null!;
    }

    public class CommandInfoUpsert(CommandInfoUpsertData data) : Command<CommandInfoUpsertData>(CommandName.Info_Upsert, data)
    {
    }
}