using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptainSonar.Server.Engine
{
    public class StateDiagnosticsCode
    {
        private static readonly Dictionary<int, string> DiagnosticMessage = new() {
            // Exceptions
            { 1000, "An exception occurred."},
            { 1001, "Player is required to start the game." },
            { 1002, "Game is already finished." },
            { 1003, "Game is already started." },
            { 1004, "Game is not started yet." },
            { 1005, "Players are already added to the game. State is invalid." },
            { 1006, "Player is the last player in the game." },
            { 1007, "Player is already in the team." },
            { 1008, "Coordinate is not in bounds." },
            { 1009, "Coordinate is on top of an obstacle." },
            { 1010, "Coordinate is on top of player's own path." },
            { 1011, "Moving to the coordinate is not allowed." },
            { 1012, "Player has not moved yet." },
            { 1013, "Room unit position id is not valid." },
            { 1014, "Room unit type is not valid." },
            { 1015, "Unlinked room units can not be repaired by type." },
            { 1016, "Linked room units can only be repaired when they are all damaged." },
            { 1017, "Asset with the given name does not exist." },
            { 1018, "Asset is already full." },
            { 1019, "Putting mine on your own path is forbidden." },
            { 1020, "Putting mine on an obstacle is forbidden." },
            { 1021, "Player can not use an empty asset. Asset needs to be loaded first." },
            { 1022, "The distance is not allowed to deploy the asset." },
            { 1023, "Provided coordinate list is invalid. Some of the coordinates are either out of bounds, are on an obstacle, are not adjacent or intersects with player's own path." },
            { 1024, "Player's last coordinate and the first coordinate provided in the list are not adjacent." },
            { 1025, "A mine already exists in that location." },
            { 1026, "A mine does not exist on given coordinates." },
            { 1027, "Either info text or the locations should be provided." },
            { 1028, "String length can not exceed the limits." },
            { 1029, "Index is not valid." },
            { 1030, "One of the coordinates in the given list is not adjacent." },
            // Informatives
            { 2001, "Coordinate is on a mine" },
            { 2002, "Coordinate is on a self put mine" },
            { 2003, "Coordinate is on an enemy mine" },
        };

        public static string GetDiagnosticMessageByCode(int code)
        {
            return DiagnosticMessage[code];
        }
    }
}
