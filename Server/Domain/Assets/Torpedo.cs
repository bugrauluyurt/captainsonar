﻿using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Torpedo(int slotSize = 3) : Asset(AssetType.Attack, AssetName.Torpedo, slotSize)
    {
    }
}
