using CaptainSonar.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Assets
{
    internal class AssetSlots(int size)
    {
        private readonly int _size = size;
        private int _currentSize = 0;

        private bool IsFilled => _currentSize == _size;
        private bool IsEmpty => _currentSize == 0;

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

    internal abstract class Asset(Player owner, AssetType type, int size)
    {
        private readonly Player _owner = owner;
        public readonly AssetType Type = type;
        public readonly AssetSlots Slots = new(size);

        public Player GetOwner() => _owner;
    }
}
