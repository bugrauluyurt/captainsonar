using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class Health(Player owner, int slotSize = 4) : Asset(owner, AssetType.Health, ActionType.None, slotSize)
    {
        public bool IsAlive => !Slots.IsFilled;
        public bool IsDead => !IsAlive;

        public void Damage()
        {
            if (IsDead)
            {
                throw new InvalidOperationException("Health is already depleted");
            }

            Slots.Increase();
        }
    }
}