using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeployMineData
    {
        public required TeamName TeamName { get; set; }
        public required Coordinate Coordinate { get; set; } = null!;
    }

    public class CommandAssetDeployMine(CommandAssetDeployMineData data) : Command<CommandAssetDeployMineData>(CommandName.Asset_Deploy_Mine, data)
    {
    }
}