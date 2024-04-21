using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDetonateMineData
    {
        public TeamName TeamName { get; set; }
        public Coordinate Coordinate { get; set; } = null!;
    }

    public class CommandAssetDetonateMine(CommandAssetDetonateMineData data) : Command<CommandAssetDetonateMineData>(CommandName.Asset_Detonate_Mine, data)
    {
    }
}