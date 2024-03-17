using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public enum AssetType
    {
        Health, // Health
        Attack, // Mine, Torpedo
        Radar, // Drone, Sonar
        Passive, // Silence, Scenario,
        Nuclear, // Nuclear
    }
}
