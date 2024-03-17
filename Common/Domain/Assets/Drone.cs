using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class Drone(int slotSize = 4) : Asset(AssetType.Radar, AssetName.Drone, slotSize)
    {
    }
}
