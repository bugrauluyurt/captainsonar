using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Game
{
    public class Player : IComparable<Player>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public PlayerRole Role { get; set; }

        public int CompareTo(Player? other)
        {
            // If the provided object is null, this instance comes first.
            if (other == null)
                return 1;

            // Compare the Ids of the players
            return string.Compare(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Player other = (Player)obj;
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
