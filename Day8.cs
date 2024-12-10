namespace AdventOfCode2024;

public static class Day8
{
    public static long Part1(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);

        var antennas = FindAntennas(input);
        var range = new Utilities.Coordinate(input.Count, input[0].Length);

        var allAntiNodes = new List<Utilities.Coordinate>();
        foreach (var antenna in antennas)
        {
            antenna.AntiNodes = FindAntiNodes(antenna.Locations, range);
            allAntiNodes.AddRange(antenna.AntiNodes);
        }

        var distinctAntiNodes = allAntiNodes.DistinctBy(x => $"{x.X},{x.Y}").ToList();

        return distinctAntiNodes.Count;
    }

    public static long Part2(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);

        var antennas = FindAntennas(input);
        var range = new Utilities.Coordinate(input.Count, input[0].Length);

        var allAntiNodes = new List<Utilities.Coordinate>();
        foreach (var antenna in antennas)
        {
            antenna.AntiNodes = FindMoreAntiNodes(antenna.Locations, range);
            allAntiNodes.AddRange(antenna.Locations);
            allAntiNodes.AddRange(antenna.AntiNodes);
        }

        var distinctAntiNodes = allAntiNodes.DistinctBy(c => $"{c.X},{c.Y}").ToList();

        return distinctAntiNodes.Count;
    }

    private static List<Antenna> FindAntennas(List<string> input)
    {
        var antennas = new List<Antenna>();

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var frequency = input[y][x];
                if (frequency != '.')
                {
                    var coordinates = new Utilities.Coordinate(x, y);
                    var antenna = antennas.FirstOrDefault(a => a.Frequency == frequency);
                    if (antenna != null)
                    {
                        antenna.Locations.Add(coordinates);
                    }
                    else
                    {
                        antennas.Add(new Antenna(frequency, new List<Utilities.Coordinate> { coordinates }));
                    }
                }
            }
        }

        return antennas;
    }

    private static bool InRange(Utilities.Coordinate coordinate, Utilities.Coordinate range)
    {
        return coordinate is { X: >= 0, Y: >= 0 } && coordinate.X < range.X && coordinate.Y < range.Y;
    }

    private static List<Utilities.Coordinate> FindAntiNodes(List<Utilities.Coordinate> locations, Utilities.Coordinate range)
    {
        var antiNodes = new List<Utilities.Coordinate>();
        for (var i = 0; i < locations.Count - 1; i++)
        {
            for (var j = i + 1; j < locations.Count; j++)
            {
                var xDiff = locations[i].X - locations[j].X;
                var yDiff = locations[i].Y - locations[j].Y;
                var antiNode1 = new Utilities.Coordinate(locations[i].X + xDiff, locations[i].Y + yDiff);
                var antiNode2 = new Utilities.Coordinate(locations[j].X - xDiff, locations[j].Y - yDiff);

                if (InRange(antiNode1, range))
                {
                    antiNodes.Add(antiNode1);
                }

                if (InRange(antiNode2, range))
                {
                    antiNodes.Add(antiNode2);
                }
            }
        }

        return antiNodes;
    }

    private static List<Utilities.Coordinate> FindMoreAntiNodes(List<Utilities.Coordinate> locations, Utilities.Coordinate range)
    {
        var antiNodes = new List<Utilities.Coordinate>();
        for (var i = 0; i < locations.Count - 1; i++)
        {
            for (var j = i + 1; j < locations.Count; j++)
            {
                var xDiff = locations[i].X - locations[j].X;
                var yDiff = locations[i].Y - locations[j].Y;
                var startX = locations[i].X;
                var startY = locations[i].Y;

                while (InRange(new Utilities.Coordinate(startX + xDiff, startY + yDiff), range))
                {
                    var nextX = startX + xDiff;
                    var nextY = startY + yDiff;
                    var antiNode = new Utilities.Coordinate(nextX, nextY);
                    antiNodes.Add(antiNode);
                    startX = nextX;
                    startY = nextY;
                }


                startX = locations[j].X;
                startY = locations[j].Y;
                while (InRange(new Utilities.Coordinate(startX - xDiff, startY - yDiff), range))
                {
                    var nextX = startX - xDiff;
                    var nextY = startY - yDiff;
                    var antiNode = new Utilities.Coordinate(nextX, nextY);
                    antiNodes.Add(antiNode);
                    startX = nextX;
                    startY = nextY;
                }
            }
        }

        return antiNodes;
    }

    private class Antenna
    {
        public Antenna(char frequency, List<Utilities.Coordinate> locations)
        {
            Frequency = frequency;
            Locations = locations;
            AntiNodes = new List<Utilities.Coordinate>();
        }

        public char Frequency { get; set; }
        public List<Utilities.Coordinate> Locations { get; set; }
        public List<Utilities.Coordinate> AntiNodes { get; set; }
    }

    public static void DrawMap(List<string> input, List<Utilities.Coordinate> antiNodes)
    {
        foreach (var antiNode in antiNodes)
        {
            var newLine = input[antiNode.Y].Remove(antiNode.X, 1).Insert(antiNode.X, "#");
            input[antiNode.Y] = newLine;
        }

        foreach (var line in input)
        {
            Console.WriteLine(line);
        }
    }
}