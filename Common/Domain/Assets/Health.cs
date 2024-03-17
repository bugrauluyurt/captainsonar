using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class Health(int slotSize = 4) : Asset(AssetType.Health, AssetName.Health, slotSize, false)
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