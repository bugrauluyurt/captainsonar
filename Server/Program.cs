// See https://aka.ms/new-console-template for more information

using CaptainSonar.Common.Domain.Commands;
using CaptainSonar.Common.Domain.Game;
using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Server.Engine;
using Dumpify;

// Player1 Dots
var player1Dots = new List<Dot>
{
    new(new Coordinate(3, 5), null),
    new(new Coordinate(4, 5), null)
};


// Coordinate list and map grid
var grid = new Grid(GridType.Alpha);
grid.PrintGrid(dotsMarked: player1Dots);

var startCoordinate = new Coordinate(2, 1);
var endCoordinate = new Coordinate(2, 3);

var shortestPathCount = Pathfinder.FindShortestPathCount(grid.GetDots(), startCoordinate, endCoordinate);

Console.WriteLine($"The shortest path from {startCoordinate.Row}:{startCoordinate.Column} to {endCoordinate.Row}:{endCoordinate.Column} is {shortestPathCount} moves");

// State machine

// [1] State execution step initialization
StateExecutionStep stateExecutionStep = new()
{
    State = StateHelper.CreateState(grid),
    Commands = []
};

// [Command] Session start
var player1 = new Player() { Name = "Player 1", Id = "1" };
var commandSessionStartData = new CommandSessionStartData() { Player = player1 };
var commandSessionStart = new CommandSessionStart(commandSessionStartData);
StateExecutionStep step1 = StateMachine.ExecCommand(commandSessionStart, stateExecutionStep);
// step1.Dump();