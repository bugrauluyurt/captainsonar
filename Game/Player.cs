using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Game
{
    internal class Player
    {
        public required string Name { get; set; }
        public required PlayerRole PlayerRole { get; set; }
    }
}
