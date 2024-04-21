using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeployTorpedoData
    {
        public TeamName TeamName { get; set; }
        public Coordinate Coordinate { get; set; } = null!;
    }

    public class CommandAssetDeployTorpedo(CommandAssetDeployTorpedoData data) : Command<CommandAssetDeployTorpedoData>(CommandName.Asset_Deploy_Torpedo, data)
    {
    }
}