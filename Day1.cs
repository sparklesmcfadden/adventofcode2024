namespace AdventOfCode2024;

public static class Day1
{
    private const string InputPath = "../../../inputs/day1.txt";

    public static int Day1_Part1()
    {
        var file = Utilities.LoadFileAsLines(InputPath);

        var listA = MakeList(file, 0);
        var listB = MakeList(file, 1);

        int diffSum = listA.Zip(listB, (a, b) => Math.Abs(a - b)).Sum();
        
        return diffSum;
    }

    public static int Day1_Part2()
    {
        var file = Utilities.LoadFileAsLines(InputPath);

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
        var count = 0;
        foreach (var item in inputList)
        {
            if (input == item)
            {
                count++;
            }
        }

        return input * count;
    }
}