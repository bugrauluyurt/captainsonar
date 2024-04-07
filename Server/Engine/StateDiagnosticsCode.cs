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
            { 1001, "Player is required to start the game" },
            { 1002, "Game is already finished" },
            { 1003, "Game is already started" },
            { 1004, "Game is not started yet" },
            { 1005, "Players are already added to the game. State is invalid." },
            { 1006, "Player is the last player in the game" },
            { 1007, "Player is already in the team" },
            { 1008, "Coordinate is not in bounds" },
            { 1009, "Coordinate is on top of an obstacle" },
            { 1010, "Coordinate is on top of player's own path" },
            { 1011, "Moving to the coordinate is not allowed" },
            { 1012, "Player has not moved yet" },
            { 1013, "Room unit position id is not valid" },
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
