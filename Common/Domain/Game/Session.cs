using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Game
{
    public class Session(string id, string ownerId)
    {
        public required string Id { get; set; } = id;
        public required string OwnerId { get; set; } = ownerId;
    }
}
