namespace AdventOfCode2024;

public static class Day10
{

    public static int Part1(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);
        var map = InputToMap(input);
        var counter = 0;

        var trailheads = GetTrailheads(map);

        foreach (var trailhead in trailheads)
        {
            var markers = new List<Utilities.Coordinate> { trailhead };
            for (var i = 0; i < markers.Count; i++)
            {
                var startMarker = markers[i];
                var newMarkers = FindNextTrailMarkers(map, startMarker);
                markers.AddRange(newMarkers);
            }

            counter += CheckMarkers(map, markers);
        }

        return counter;
    }

    public static int Part2(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);
        var map = InputToMap(input);
        var counter = 0;

        var markers = GetTrailheads(map);

        for (var i = 0; i < markers.Count; i++)
        {
            var startMarker = markers[i];
            var newMarkers = FindNextTrailMarkers(map, startMarker);
            markers.AddRange(newMarkers);
            counter += CheckMarkers(map, newMarkers);
        }
        
        return counter;
    }

    private static List<int[]> InputToMap(List<string> input)
    {
        return input.Select(n => n.Select(a => a == '.' ? -1 : int.Parse(a.ToString())).ToArray()).ToList();
    }

    private static List<Utilities.Coordinate> GetTrailheads(List<int[]> input)
    {
        var trailheads = new List<Utilities.Coordinate>();
        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 0)
                {
                    trailheads.Add(new Utilities.Coordinate(x, y));
                }
            }
        }

        return trailheads;
    }

    private static List<Utilities.Coordinate> FindNextTrailMarkers(List<int[]> map, Utilities.Coordinate start)
    {
        var nextMarkers = new List<Utilities.Coordinate>();
        var startMarker = map[start.Y][start.X];

        foreach (var direction in Utilities.Directions)
        {
            var next = start.Move(direction);
            if (next.Y >= map.Count || next.Y < 0 || next.X >= map[0].Length || next.X < 0)
            {
                continue;
            }

            if (map[next.Y][next.X] == startMarker + 1)
            {
                nextMarkers.Add(new Utilities.Coordinate(next.X, next.Y));
            }
        }

        return nextMarkers;
    }

    private static int CheckMarkers(List<int[]> map, List<Utilities.Coordinate> markers)
    {
        var distinctMarkers = markers.DistinctBy(x => x.AsString()).ToList();
        return distinctMarkers.Count(marker => map[marker.Y][marker.X] == 9);
    }
}