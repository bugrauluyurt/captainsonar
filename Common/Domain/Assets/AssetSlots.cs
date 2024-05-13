using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Assets
{
    public class AssetSlots(int size)
    {
        private readonly int _size = size;
        private int _currentSize = 0;

        public bool IsLoaded => _currentSize == _size;
        public bool IsEmpty => _currentSize == 0;

        public void Load()
        {
            if (IsLoaded)
            {
                throw new InvalidOperationException("Asset slot is already loaded");
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

        public int GetRemainingSize()
        {
            return _size - _currentSize;
        }

        public int GetCurrentSize()
        {
            return _currentSize;
        }

        public int GetTotalSize()
        {
            return _size;
        }
    }
}