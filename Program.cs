// See https://aka.ms/new-console-template for more information
using CaptainSonar.Map;

var map = new Map(MapType.Alpha);
map.PrintMap();

var startCoordinate = new Coordinate(1, 1);
var endCoordinate = new Coordinate(1, 1);

var shortestPathCount = Pathfinder.FindShortestPathCount(map.Dots, startCoordinate, endCoordinate);

Console.WriteLine($"The shortest path from {startCoordinate.Row}:{startCoordinate.Column} to {endCoordinate.Row}:{endCoordinate.Column} is {shortestPathCount} moves");