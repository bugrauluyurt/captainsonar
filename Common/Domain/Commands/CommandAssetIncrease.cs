using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetIncreaseData
    {
        public AssetName AssetName { get; set; }
        public TeamName TeamName { get; set; }
    }

    public class CommandAssetIncrease(CommandAssetIncreaseData data) : Command<CommandAssetIncreaseData>(CommandName.Asset_Increase, data)
    {
    }
}