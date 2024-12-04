using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static class Day3
{
    private const string InputPath = "../../../inputs/day3.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine("Day 3");
        Timer.Start();
        var part1 = Day3_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Day3_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static int Day3_Part1()
    {
        var input = Utilities.LoadFile(InputPath);
        var matches = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");

        return matches.Select(match =>
        {
            var products = Regex.Matches(match.Value, "\\d+").Select(n => Int32.Parse(n.Value)).ToList();
            return products[0] * products[1];
        }).Sum();
    }

    private static int Day3_Part2()
    {
        var input = Utilities.LoadFile(InputPath);
        var matches = Regex.Matches(input, "mul\\(\\d+,\\d+\\)|do\\(\\)|don't\\(\\)");
        var enabled = true;

        return matches.Select(match =>
        {
            switch (match.Value)
            {
                case "do()":
                    enabled = true;
                    return 0;
                case "don't()":
                    enabled = false;
                    return 0;
                default:
                {
                    if (enabled)
                    {
                        var products = Regex.Matches(match.Value, "\\d+").Select(n => Int32.Parse(n.Value)).ToList();
                        return products[0] * products[1];
                    }

                    return 0;
                }
            }
        }).Sum();
    }
}