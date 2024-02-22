using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Map
{
    internal record Coordinate
    {
        public readonly int Column;
        public readonly int Row;

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"{Row}:{Column}";
        }
    }
}
