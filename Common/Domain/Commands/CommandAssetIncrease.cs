using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetIncreaseData
    {
        public required AssetName AssetName { get; set; }
        public required TeamName TeamName { get; set; }
    }

    public class CommandAssetIncrease(CommandAssetIncreaseData data) : Command<CommandAssetIncreaseData>(CommandName.Asset_Increase, data)
    {
    }
}