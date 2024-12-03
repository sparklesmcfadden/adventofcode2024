using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day3
{
    private const string InputTestPath = "../../../inputs/day3_test.txt";
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

    public static int Day3_Part1()
    {
        var input = Utilities.LoadFile(InputPath);
        var matches = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");
        var result = 0;

        foreach (Match match in matches)
        {
            var products = Regex.Matches(match.Value, "\\d+").Select(n => Int32.Parse(n.Value)).ToList();
            result += products[0] * products[1];
        }

        return result;
    }

    public static int Day3_Part2()
    {
        var input = Utilities.LoadFile(InputPath);
        var matches = Regex.Matches(input, "mul\\(\\d+,\\d+\\)|do\\(\\)|don't\\(\\)");

        var result = 0;
        var enabled = true;
        foreach (Match match in matches)
        {
            enabled = match.Value switch
            {
                "do()" => true,
                "don't()" => false,
                _ => enabled
            };

            if (enabled && match.Value.StartsWith("mul"))
            {
                var products = Regex.Matches(match.Value, "\\d+").Select(n => Int32.Parse(n.Value)).ToList();
                result += products[0] * products[1];
            }
        }

        return result;
    }
}