using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class Silence(int slotSize = 6) : Asset(AssetType.Passive, AssetName.Silence, slotSize)
    {
    }
}
