using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class AssetHelpers
    {
        public static List<Asset> CreateAssets() =>
        [
            new Health(),
            new Drone(),
            new Mine(),
            new Silence(),
            new Scenario(),
            new Torpedo(),
            new Sonar(),
        ];
    }
}
