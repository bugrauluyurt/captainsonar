using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeploySonarData
    {
        public TeamName TeamName { get; set; }
    }

    public class CommandAssetDeploySonar(CommandAssetDeploySonarData data) : Command<CommandAssetDeploySonarData>(CommandName.Asset_Deploy_Drone, data)
    {
    }
}