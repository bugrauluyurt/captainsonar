using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeploySilenceData
    {
        public TeamName TeamName { get; set; }
        public List<Coordinate> Coordinates { get; set; } = null!;
    }

    public class CommandAssetDeploySilence(CommandAssetDeploySilenceData data) : Command<CommandAssetDeploySilenceData>(CommandName.Asset_Deploy_Silence, data)
    {
    }
}