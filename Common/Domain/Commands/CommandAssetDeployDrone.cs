using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandAssetDeployDroneData
    {
        public TeamName TeamName { get; set; }
    }

    public class CommandAssetDeployDrone(CommandAssetDeployDroneData data) : Command<CommandAssetDeployDroneData>(CommandName.Asset_Deploy_Drone, data)
    {
    }
}