using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common;

namespace CaptainSonar.Common.Domain.Game
{
    public class Team
    {
        public TeamName Name { get; set; }
        public List<Player> Players { get; set; } = [];
    }
}
