using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Game;
using CaptainSonar.Map;

namespace CaptainSonar.Assets
{
    internal class Mine(Player owner, Coordinate location, bool shouldActivateOnInit = true) : Asset(owner, AssetType.Attack, ActionType.None, 3)
    {
        // @TODO: Move these properties to into a class which belongs to the Game namespace.

        //private readonly Coordinate _location;
        
        //private bool _isActive = false;

        //public Coordinate GetLocation() => _location;

        //private bool CanDetonate(Player player)
        //{
        //    return _isActive && GetOwner().Id == player.Id;
        //}

        //public void Activate()
        //{
        //    _isActive = true;
        //}

        //public bool IsActivated()
        //{
        //    return _isActive;
        //}

        //public bool Detonate(Game.Player player)
        //{
        //    if (!CanDetonate(player)) {
        //        Console.WriteLine("The player cannot detonate this mine");
        //        return false;
        //    }
        //    // @TODO: Remove the loggin into an external class
        //    Console.WriteLine($"The mine located at {Map.Helpers.GetReadableCoordinate(_location)} has been detonated by {player.Name}");
        //    return true;
        //}
    }
}
