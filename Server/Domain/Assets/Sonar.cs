using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Sonar(int slotSize = 3) : Asset(AssetType.Radar, AssetName.Sonar, slotSize)
    {
    }
}
