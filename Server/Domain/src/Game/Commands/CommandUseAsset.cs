using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Assets;

namespace CaptainSonar.Game.Commands
{
    class CommandUseAssetData
    {
        public AssetName AssetName { get; }
    }
    internal class CommandUseAsset(CommandUseAssetData data) : Command<CommandUseAssetData>(data, CommandName.UseAsset)
    {
    }
}
