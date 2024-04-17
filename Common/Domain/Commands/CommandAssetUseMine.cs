using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetUseMineData
    {
        public TeamName TeamName { get; set; }
        public Coordinate Coordinate { get; set; } = null!;
    }

    public class CommandAssetUseMine(CommandAssetUseMineData data) : Command<CommandAssetUseMineData>(CommandName.Asset_Use_Mine, data)
    {
    }
}