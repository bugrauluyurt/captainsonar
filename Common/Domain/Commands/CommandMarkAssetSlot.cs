using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandMarkAssetSlotData
    {
        public AssetName AssetName { get; }
    }

    public class CommandMarkAssetSlot(CommandMarkAssetSlotData data) : Command<CommandMarkAssetSlotData>(data, CommandName.MarkAssetSlot)
    {
    }
}
