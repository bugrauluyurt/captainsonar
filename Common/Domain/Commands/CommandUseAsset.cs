using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandUseAssetData
    {
        public AssetName AssetName { get; }
    }

    public class CommandUseAsset(CommandUseAssetData data) : Command<CommandUseAssetData>(data, CommandName.UseAsset)
    {
    }
}
