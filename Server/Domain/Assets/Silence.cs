using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Silence(int slotSize = 6) : Asset(AssetType.Passive, AssetName.Silence, slotSize)
    {
    }
}
