using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Drone(int slotSize = 4) : Asset(AssetType.Radar, AssetName.Drone, slotSize)
    {
    }
}
