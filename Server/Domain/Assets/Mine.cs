using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Game;
using CaptainSonar.Map;

namespace CaptainSonar.Assets
{
    internal class Mine(int slotSize = 3) : Asset(AssetType.Attack, AssetName.Mine, slotSize)
    {
    }
}
