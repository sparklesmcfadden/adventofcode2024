using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static class Day3
{
    public static int Part1(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
        var matches = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)");

        return matches.Select(match =>
        {
            var products = Regex.Matches(match.Value, "\\d+").Select(n => Int32.Parse(n.Value)).ToList();
            return products[0] * products[1];
        }).Sum();
    }

    public static int Part2(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
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