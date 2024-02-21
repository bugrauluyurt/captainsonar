﻿using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Scenario(Player owner) : Asset(owner, AssetType.Passive, ActionType.Scenario, 6)
    {
        // @TODO: Scenario has some special logic, later add it onto it if needed.
    }
}
