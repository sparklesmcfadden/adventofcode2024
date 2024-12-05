using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day5
{
    private const string Day = "5";
    private const string InputPath = $"../../../inputs/day{Day}.txt";
    private const string InputPathTest = $"../../../inputs/day{Day}_test.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine($"Day {Day}");
        Timer.Start();
        var part1 = Day5_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Day5_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static int Day5_Part1()
    {
        var inputFile = Utilities.LoadFile(InputPath).Split("\n\n");
        var rules = inputFile[0].Split("\n").Select(n => n.Split("|").ToList()).ToList();
        var pages = inputFile[1].Split("\n").Select(n => n.Split(",").ToList()).ToList();

        return pages.Where(page => ValidateRules(page, rules)).Sum(page => Int32.Parse(page[page.Count / 2]));
    }

    private static int Day5_Part2()
    {
        var inputFile = Utilities.LoadFile(InputPath).Split("\n\n");
        var rules = inputFile[0].Split("\n").Select(n => n.Split("|").ToList()).ToList();
        var pages = inputFile[1].Split("\n").Select(n => n.Split(",").ToList()).ToList();
        var middleSum = 0;

        foreach (var page in pages)
        {
            if (!ValidateRules(page, rules))
            {
                middleSum += Int32.Parse(FixPage(page, rules)[page.Count / 2]);
            }
        }
        
        return middleSum;
    }

    private static bool ValidateRules(List<string> page, List<List<string>> rules)
    {
        return rules.All(rule => ValidateRule(page, rule));
    }

    private static bool ValidateRule(List<string> page, List<string> rule)
    {
        return page.IndexOf(rule[0]) <= page.IndexOf(rule[1]) || page.IndexOf(rule[0]) < 0 || page.IndexOf(rule[1]) < 0;
    }

    private static List<string> FixPage(List<string> page, List<List<string>> rules)
    {
        var fixedPage = page.Select(n => n).ToList();

        var isFixed = ValidateRules(fixedPage, rules);
        while (!isFixed)
        {
            foreach (var rule in rules)
            {
                if (!ValidateRule(fixedPage, rule))
                {
                    var previousPage = fixedPage.Select(n => n).ToList();
                    fixedPage.RemoveAt(previousPage.IndexOf(rule[1]));
                    fixedPage.Insert(previousPage.IndexOf(rule[0]), rule[1]);
                }
            }
            isFixed = ValidateRules(fixedPage, rules);
        }
        
        return fixedPage;
    }
}