// See https://aka.ms/new-console-template for more information
using CaptainSonar.Map;

var grid = new Grid(GridType.Alpha);
grid.PrintGrid();

var startCoordinate = new Coordinate(1, 1);
var endCoordinate = new Coordinate(1, 1);

var shortestPathCount = Pathfinder.FindShortestPathCount(grid.Dots, startCoordinate, endCoordinate);

Console.WriteLine($"The shortest path from {startCoordinate.Row}:{startCoordinate.Column} to {endCoordinate.Row}:{endCoordinate.Column} is {shortestPathCount} moves");