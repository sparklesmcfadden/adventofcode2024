namespace AdventOfCode2024;

public class Day1
{
    public static int RunDay1(string path)
    {
        var file = Utilities.LoadFileAsLines(path);

        List<int> listA;
        List<int> listB;

        int diffSum = 0;

        listA = file.Select(l => Int32.Parse(l.Split("   ")[0])).Order().ToList();
        listB = file.Select(l => Int32.Parse(l.Split("   ")[1])).Order().ToList();

        foreach (var (number, i) in listA.Select((v, i) => (v, i)))
        {
            var diff = Math.Abs(number - listB[i]);
            diffSum += diff;
        }
        
        Console.WriteLine(diffSum);
        return diffSum;
    }
}