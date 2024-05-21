using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDetonateMineData
    {
        public required TeamName TeamName { get; init; }
        public required Coordinate Coordinate { get; init; }
    }

    public class CommandAssetDetonateMine(CommandAssetDetonateMineData data) : Command<CommandAssetDetonateMineData>(CommandName.Asset_Detonate_Mine, data)
    {
    }
}