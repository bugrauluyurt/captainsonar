using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class Torpedo(int slotSize = 3) : Asset(AssetType.Attack, AssetName.Torpedo, slotSize)
    {
    }
}
