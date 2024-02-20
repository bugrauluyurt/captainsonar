using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Map;
using CaptainSonar;

namespace CaptainSonar.Assets
{
    internal class Mine
    {
        private Coordinate _location;
        private CaptainSonar.Player.Player _owner;
        private bool _isActive = false;

        public Mine(CaptainSonar.Player.Player owner, Coordinate location, bool shouldActivateOnInit = true)
        {
            _location = location;
            _owner = owner;
            if (shouldActivateOnInit)
            {
                Activate();
            }
        }

        public Coordinate GetLocation() => _location;
        public CaptainSonar.Player.Player GetOwner() => _owner;

        private bool CanDetonate(CaptainSonar.Player.Player player)
        {
            return _isActive && _owner == player;
        }

        public void Activate()
        {
            _isActive = true;
        }

        public bool IsActivated()
        {
            return _isActive;
        }

        public bool Detonate(CaptainSonar.Player.Player player)
        {
            if (!CanDetonate(player)) {
                Console.WriteLine("The player cannot detonate this mine");
                return false;
            }
            Console.WriteLine($"The mine located at {_location.Row}:{_location.Column} has been detonated by {player.Name}");
            return true;
        }
    }
}
