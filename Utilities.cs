namespace AdventOfCode2024;

public static class Utilities
{
    private static IDictionary<Direction, int[]> DirectionDeltas { get; } = new Dictionary<Direction, int[]>
    {
        { Direction.North, [0, -1] },
        { Direction.Northeast,[1, -1] },
        { Direction.East, [1, 0] },
        { Direction.Southeast, [1, 1] },
        { Direction.South, [0, 1] },
        { Direction.Southwest, [-1, 1] },
        { Direction.West, [-1, 0] },
        { Direction.Northwest, [-1, -1] }
    };
    
    public static List<Direction> CardinalDirections { get; } =
    [
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West
    ];
    
    public static List<Direction> Directions { get; } =
    [
        Direction.North,
        Direction.Northeast,
        Direction.East,
        Direction.Southeast,
        Direction.South,
        Direction.Southwest,
        Direction.West,
        Direction.Northwest
    ];
    
    public static string LoadCsvFile(string path)
    {
        var file = File.ReadAllLines(path);
        return file[0];
    }

    public static List<string> LoadFileAsLines(string path)
    {
        var file = File.ReadAllLines(path);
        return file.ToList();
    }

    public static string LoadFile(string path)
    {
        return File.ReadAllText(path);
    }
    
    public class Coordinate(int x, int y)
    {
        public string AsString()
        {
            return $"{X}{Y}";
        }

        public Coordinate Move(Direction direction)
        {
            var delta = DirectionDeltas[direction];
            return new Coordinate(X + delta[0], Y + delta[1]);
        }

        public int X { get; } = x;
        public int Y { get; } = y;
    }

    public enum Direction
    {
        North,
        Northeast,
        East,
        Southeast,
        South,
        Southwest,
        West,
        Northwest
    }
}