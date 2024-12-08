namespace AdventOfCode2024;

public static class Day1
{
    public static int Part1(string inputPath)
    {
        var file = Utilities.LoadFileAsLines(inputPath);

        var listA = MakeList(file, 0);
        var listB = MakeList(file, 1);

        int diffSum = listA.Zip(listB, (a, b) => Math.Abs(a - b)).Sum();
        
        return diffSum;
    }

    public static int Part2(string inputPath)
    {
        var file = Utilities.LoadFileAsLines(inputPath);

        var listA = MakeList(file, 0);
        var listB = MakeList(file, 1);
        
        var similarityScore = listA.Select(n => IsInList(n, listB)).Sum();
        
        return similarityScore;
    }

    private static List<int> MakeList(List<string> input, int index)
    {
        return input.Select(l => Int32.Parse(l.Split("   ")[index])).Order().ToList();
    }
    
    private static int IsInList(int input, List<int> inputList)
    {
        var count = inputList.Count(n => n == input);

        return input * count;
    }
}