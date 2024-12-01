namespace AdventOfCode2024;

public class Utilities
{
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
}