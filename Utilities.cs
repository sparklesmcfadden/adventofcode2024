namespace AdventOfCode2024;

public class Utilities
{
    
    public static readonly List<int[]> Directions =
    [
        [0, 1],
        [-1, 0],
        [0, -1],
        [1, 0]
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

        public Coordinate Move(int[] direction)
        {
            return new Coordinate(X + direction[0], Y + direction[1]);
        }

        public int X { get; } = x;
        public int Y { get; } = y;
    }
}