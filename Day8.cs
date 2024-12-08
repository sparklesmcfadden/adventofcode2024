namespace AdventOfCode2024;

public static class Day8
{
    public static long Part1(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);

        var antennas = FindAntennas(input);
        var range = new Coordinates(input.Count, input[0].Length);

        var allAntiNodes = new List<Coordinates>();
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
        var range = new Coordinates(input.Count, input[0].Length);

        var allAntiNodes = new List<Coordinates>();
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
                    var coordinates = new Coordinates(x, y);
                    var antenna = antennas.FirstOrDefault(a => a.Frequency == frequency);
                    if (antenna != null)
                    {
                        antenna.Locations.Add(coordinates);
                    }
                    else
                    {
                        antennas.Add(new Antenna(frequency, new List<Coordinates> { coordinates }));
                    }
                }
            }
        }

        return antennas;
    }

    private static bool InRange(Coordinates coordinates, Coordinates range)
    {
        return coordinates is { X: >= 0, Y: >= 0 } && coordinates.X < range.X && coordinates.Y < range.Y;
    }

    private static List<Coordinates> FindAntiNodes(List<Coordinates> locations, Coordinates range)
    {
        var antiNodes = new List<Coordinates>();
        for (var i = 0; i < locations.Count - 1; i++)
        {
            for (var j = i + 1; j < locations.Count; j++)
            {
                var xDiff = locations[i].X - locations[j].X;
                var yDiff = locations[i].Y - locations[j].Y;
                var antiNode1 = new Coordinates(locations[i].X + xDiff, locations[i].Y + yDiff);
                var antiNode2 = new Coordinates(locations[j].X - xDiff, locations[j].Y - yDiff);

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

    private static List<Coordinates> FindMoreAntiNodes(List<Coordinates> locations, Coordinates range)
    {
        var antiNodes = new List<Coordinates>();
        for (var i = 0; i < locations.Count - 1; i++)
        {
            for (var j = i + 1; j < locations.Count; j++)
            {
                var xDiff = locations[i].X - locations[j].X;
                var yDiff = locations[i].Y - locations[j].Y;
                var startX = locations[i].X;
                var startY = locations[i].Y;

                while (InRange(new Coordinates(startX + xDiff, startY + yDiff), range))
                {
                    var nextX = startX + xDiff;
                    var nextY = startY + yDiff;
                    var antiNode = new Coordinates(nextX, nextY);
                    antiNodes.Add(antiNode);
                    startX = nextX;
                    startY = nextY;
                }


                startX = locations[j].X;
                startY = locations[j].Y;
                while (InRange(new Coordinates(startX - xDiff, startY - yDiff), range))
                {
                    var nextX = startX - xDiff;
                    var nextY = startY - yDiff;
                    var antiNode = new Coordinates(nextX, nextY);
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
        public Antenna(char frequency, List<Coordinates> locations)
        {
            Frequency = frequency;
            Locations = locations;
            AntiNodes = new List<Coordinates>();
        }

        public char Frequency { get; set; }
        public List<Coordinates> Locations { get; set; }
        public List<Coordinates> AntiNodes { get; set; }
    }

    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public static void DrawMap(List<string> input, List<Coordinates> antiNodes)
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