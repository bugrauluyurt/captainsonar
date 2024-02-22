using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Sonar(Player owner) : Asset(owner, AssetType.Radar, ActionType.Sonar, 3)
    {
    }
}
