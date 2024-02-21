using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Vessel
{
    internal class Vessel
    {
        public Room[] Rooms = new Room[4];

        public void AddRoom(Room room, int index)
        {
            Rooms[index] = room;
        }

    }
}
