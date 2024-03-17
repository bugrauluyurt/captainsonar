using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class Scenario(int slotSize = 6) : Asset(AssetType.Passive, AssetName.Scenario, slotSize)
    {
    }
}
