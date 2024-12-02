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

    public static void PrintResults(int day, List<object> results)
    {
        Console.WriteLine($"Day {day}:");
        results.ForEach(n => Console.WriteLine(n.ToString()));
    }
}