using System.Diagnostics;

namespace AdventOfCode2024;

public class AdventRunner
{
    private readonly string _inputPath;
    private readonly int _day;
    private static readonly Stopwatch Timer = new();
    
    public AdventRunner(string inputPath, int day, bool test = false)
    {
        var inputData = $"{inputPath}day{day}.txt";
        var inputTestData = $"{inputPath}day{day}_test.txt";
        _inputPath = test ? inputTestData : inputData;
        _day = day;
        Timer.Reset();
    }

    public void Run()
    {
        Console.WriteLine($"Day {_day}");
        Timer.Start();
        var part1 = Part1(_inputPath, _day);
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Part2(_inputPath, _day);
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private long Part1(string inputPath, int day)
    {
        return day switch
        {
            1 => Day1.Part1(inputPath),
            2 => Day2.Part1(inputPath),
            3 => Day3.Part1(inputPath),
            4 => Day4.Part1(inputPath),
            5 => Day5.Part1(inputPath),
            6 => Day6.Part1(inputPath),
            7 => Day7.Part1(inputPath),
            8 => Day8.Part1(inputPath),
            _ => 0
        };
    }

    private static long Part2(string inputPath, int day)
    {
        return day switch
        {
            1 => Day1.Part2(inputPath),
            2 => Day2.Part2(inputPath),
            3 => Day3.Part2(inputPath),
            4 => Day4.Part2(inputPath),
            5 => Day5.Part2(inputPath),
            6 => Day6.Part2(inputPath),
            7 => Day7.Part2(inputPath),
            8 => Day8.Part2(inputPath),
            _ => 0
        };
    }
}