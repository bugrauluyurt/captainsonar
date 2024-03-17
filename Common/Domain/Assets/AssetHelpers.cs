using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public static class AssetHelpers
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
