using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day2
{
    private const string InputPath = "../../../inputs/day2.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine("Day 2");
        Timer.Start();
        var part1 = Day2_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");
        
        Timer.Reset();
        Timer.Start();
        var part2 = Day2_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static int Day2_Part1()
    {
        var file = Utilities.LoadFileAsLines(InputPath);
        var reportList = ParseReport(file);

        return reportList.Count(ProcessReport);
    }

    private static int Day2_Part2()
    {
        var file = Utilities.LoadFileAsLines(InputPath);
        var reportList = ParseReport(file);

        return reportList.Count(ProcessReportDampener);
    }

    private static List<List<int>> ParseReport(List<string> input)
    {
        return input.Select(l => l.Split(" ").Select(Int32.Parse).ToList()).ToList();
    }

    private static List<int> GenerateDeltas(List<int> report)
    {
        var deltas = new List<int>();

        foreach (var (level, index) in report.Select((l, i) => (l, i)))
        {
            if (index < report.Count - 1)
            {
                deltas.Add(report[index + 1] - level);
            }
        }

        return deltas;
    }

    private static bool ProcessReport(List<int> report)
    {
        var deltas = GenerateDeltas(report);

        return deltas.All(n => n != 0 && Math.Abs(n) <= 3) && (deltas.All(n => n > 0) || deltas.All(n => n < 0));
    }

    private static bool ProcessReportDampener(List<int> report)
    {
        var processedReport = ProcessReport(report);

        if (!processedReport)
        {
            for (var i = 0; i < report.Count; i++)
            {
                var tempReport = report.Select(n => n).ToList();
                tempReport.RemoveAt(i);
                if (ProcessReport(tempReport))
                {
                    return true;
                }
            }

            return false;
        }

        return processedReport;
    }
}