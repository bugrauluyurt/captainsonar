using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeploySilenceData
    {
        public required TeamName TeamName { get; init; }
        public required List<Coordinate> Coordinates { get; init; } = [];
    }

    public class CommandAssetDeploySilence(CommandAssetDeploySilenceData data) : Command<CommandAssetDeploySilenceData>(CommandName.Asset_Deploy_Silence, data)
    {
    }
}