using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandInfoUpsertData
    {
        public required TeamName TeamName { get; init; }
        public required string? Text { get; init; }
        public required Coordinate? Coordinate { get; init; }
        public required int? Index { get; init; }
    }

    public class CommandInfoUpsert(CommandInfoUpsertData data) : Command<CommandInfoUpsertData>(CommandName.Info_Upsert, data)
    {
    }
}