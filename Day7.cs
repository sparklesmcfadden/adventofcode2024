using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day7
{
    private const string Day = "7";
    private const string InputPath = $"../../../inputs/day{Day}.txt";
    private const string InputPathTest = $"../../../inputs/day{Day}_test.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine($"Day {Day}");
        Timer.Start();
        var part1 = Day7_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Day7_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static long Day7_Part1()
    {
        var input = Utilities.LoadFileAsLines(InputPath);
        var equations = GetEquations(input);

        long testValue = 0;
        foreach (var eq in equations)
        {
            var equation = eq;
            while (equation.Index < equation.Components.Count)
            {
                equation = ComputeEquation(equation);
            }

            if (equation.PreviousResults.Any(t => t == equation.TestValue))
            {
                testValue += equation.TestValue;
            }
        }

        return testValue;
    }

    private static long Day7_Part2()
    {
        var input = Utilities.LoadFileAsLines(InputPath);
        var equations = GetEquations(input);

        long testValue = 0;
        foreach (var eq in equations)
        {
            var equation = eq;
            while (equation.Index < equation.Components.Count)
            {
                equation = ComputeEquation2(equation);
            }

            if (equation.PreviousResults.Any(t => t == equation.TestValue))
            {
                testValue += equation.TestValue;
            }
        }

        return testValue;
    }

    private static Equation ComputeEquation(Equation equation)
    {
        var newResult = new List<long>();
        foreach (var result in equation.PreviousResults)
        {
            var sum = result + equation.Components[equation.Index];
            var product = result * equation.Components[equation.Index];
            newResult.Add(sum);
            newResult.Add(product);
        }

        equation.Index++;
        equation.PreviousResults = newResult;

        return equation;
    }

    private static Equation ComputeEquation2(Equation equation)
    {
        var newResult = new List<long>();
        foreach (var result in equation.PreviousResults)
        {
            var sum = result + equation.Components[equation.Index];
            var product = result * equation.Components[equation.Index];
            var concat = long.Parse($"{result}{equation.Components[equation.Index]}");
            newResult.Add(sum);
            newResult.Add(product);
            newResult.Add(concat);
        }

        equation.Index++;
        equation.PreviousResults = newResult;

        return equation;
    }

    private static List<Equation> GetEquations(List<string> input)
    {
        var equations = new List<Equation>();

        foreach (var line in input)
        {
            var splitLine = line.Split(": ");
            var testValue = long.Parse(splitLine[0]);
            var numbers = splitLine[1].Split(" ").Select(long.Parse).ToList();

            var inputLine = new Equation
            {
                TestValue = testValue,
                Components = numbers,
                PreviousResults = new List<long> { numbers[0] }
            };

            equations.Add(inputLine);
        }

        return equations;
    }

    private class Equation
    {
        public long TestValue { get; set; }
        public List<long> Components { get; set; } = new ();
        public List<long> PreviousResults { get; set; } = new();
        public int Index { get; set; } = 1;
    }
}