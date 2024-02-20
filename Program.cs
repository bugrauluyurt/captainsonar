// See https://aka.ms/new-console-template for more information
using CaptainSonar;
using CaptainSonar.Map;

var dots = MapHelpers.CreateDots(MapType.Alpha);
MapHelpers.PrintMap(dots);

var startCoordinate = new Coordinate(14, 0);
var endCoordinate = new Coordinate(12, 3);

var shortestPathCount = Pathfinder.FindShortestPathCount(dots, startCoordinate, endCoordinate);
Console.WriteLine(shortestPathCount);
// Find the shortest distance between two points
// var start = new Dot(new Coordinate(2, 5), false);
// var end = new Dot(new Coordinate(4, 5), false);

// var distance = MapHelpers.CalculateShortestDistance(dots, start, end);