using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day1
{
    private const string InputPath = "../../../inputs/day1.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine("Day 1");
        Timer.Start();
        var part1 = Day1_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");
        
        Timer.Reset();
        Timer.Start();
        var part2 = Day1_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

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
        var count = inputList.Count(n => n == input);

        return input * count;
    }
}