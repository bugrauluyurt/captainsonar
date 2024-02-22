using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Session
{
    class Session
    {
        public required string SessionName { get; set; }
        public required string SessionPassword { get; set; }
        public required string SessionId { get; set; }
        public required string SessionStatus { get; set; }
        public required Map.Grid SessionMap { get; set; }
        public required string SessionPlayers { get; set; }
    }
}
