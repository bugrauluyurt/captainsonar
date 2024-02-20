// See https://aka.ms/new-console-template for more information
using CaptainSonar;
using CaptainSonar.Map;

var dots = MapHelpers.CreateDots(MapType.Alpha);
MapHelpers.PrintMap(dots);


// Find the shortest distance between two points
var start = new Dot(new Coordinate(2, 5), false);
var end = new Dot(new Coordinate(4, 5), false);

var distance = MapHelpers.CalculateShortestDistance(dots, start, end);
if (distance == null)
{
    Console.WriteLine($"No path could be found between {Dot.GetReadableCoordinate(start.Location)} and {Dot.GetReadableCoordinate(end.Location)}.");
}
else
{
    Console.WriteLine($"Shortest distance between {Dot.GetReadableCoordinate(start.Location)} and {Dot.GetReadableCoordinate(end.Location)} is {distance.Count} moves.");
}