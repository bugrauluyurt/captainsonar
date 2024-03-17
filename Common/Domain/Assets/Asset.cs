using CaptainSonar.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Assets
{
    public class AssetSlots(int size)
    {
        private readonly int _size = size;
        private int _currentSize = 0;

        public bool IsFilled => _currentSize == _size;
        public bool IsEmpty => _currentSize == 0;

        public void Increase()
        {
            if (IsFilled)
            {
                throw new InvalidOperationException("Asset slot is already filled");
            }

            _currentSize++;
        }

        public void Empty()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Asset slot is already empty");
            }

            _currentSize = 0;
        }
    }

    public abstract class Asset(AssetType assetType, AssetName assetName, int size, bool isConsumable = true)
    {
        public readonly bool IsConsumable = isConsumable;
        public readonly AssetName AssetName = assetName;
        public readonly AssetType AssetType = assetType;
        public AssetSlots Slots = new(size);
    }
}
