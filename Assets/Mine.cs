using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Map;

namespace CaptainSonar.Assets
{
    internal class Mine
    {
        private Coordinate _location;
        private Game.Player _owner;
        private bool _isActive = false;

        public Mine(Game.Player owner, Coordinate location, bool shouldActivateOnInit = true)
        {
            _location = location;
            _owner = owner;
            if (shouldActivateOnInit)
            {
                Activate();
            }
        }

        public Coordinate GetLocation() => _location;
        public Game.Player GetOwner() => _owner;

        private bool CanDetonate(Game.Player player)
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

        public bool Detonate(Game.Player player)
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
