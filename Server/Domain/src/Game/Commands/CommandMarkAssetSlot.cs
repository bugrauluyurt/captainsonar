using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Assets;

namespace CaptainSonar.Game.Commands
{
    internal class CommandMarkAssetSlotData
    {
        public AssetName AssetName { get; }
    }

    internal class CommandMarkAssetSlot(CommandMarkAssetSlotData data) : Command<CommandMarkAssetSlotData>(data, CommandName.MarkAssetSlot)
    {
    }
}
