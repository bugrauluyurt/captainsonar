using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Game
{
    public class Player
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public PlayerRole Role { get; set; }
    }
}
