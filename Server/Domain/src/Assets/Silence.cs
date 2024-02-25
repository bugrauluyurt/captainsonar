using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Silence(Player owner) : Asset(owner, AssetType.Passive, AssetName.Silence, 6)
    {
    }
}
