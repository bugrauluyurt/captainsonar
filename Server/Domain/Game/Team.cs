using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Game
{
    internal class Team
    {
        public TeamName Name { get; set; }
        public List<Player> Players { get; set; } = [];
    }
}
