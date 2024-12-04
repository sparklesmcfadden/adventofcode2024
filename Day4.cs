using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day4
{
    private const string Day = "4";
    private const string InputPath = $"../../../inputs/day{Day}.txt";
    private const string InputPathTest = $"../../../inputs/day{Day}_test.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine($"Day {Day}");
        Timer.Start();
        var part1 = Day4_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Day4_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static int Day4_Part1()
    {
        var input = Utilities.LoadFileAsLines(InputPath);
        var count = 0;

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                count += GetWords(y, x, input, "XMAS");
            }
        }

        return count;
    }

    private static int Day4_Part2()
    {
        var input = Utilities.LoadFileAsLines(InputPath);
        var count = 0;

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                count += IsXMas(y, x, input);
            }
        }

        return count;
    }

    private static int GetWords(int y, int x, List<string> input, string word)
    {
        var count = 0;
        var startCoords = new List<int[]>();
        var directions = new List<int[]>
        {
            new [] {0, 1},
            new [] {1, 1},
            new [] {1, 0},
            new [] {1, -1},
            new [] {0, -1},
            new [] {-1, -1},
            new [] {-1, 0},
            new [] {-1, 1},
        };
        foreach (var direction in directions)
        {
            var foundWord = "";
            for (var i = 0; i < word.Length; i++)
            {
                foundWord += InRange(y + i * direction[0], x + i * direction[1], input);
            }

            if (foundWord == word)
            {
                count++;
                startCoords.Add(new [] {y, x});
            }
        }

        return count;
    }

    private static int GetWordsE(int y, int x, List<string> input)
    {
        var east = "";
        east += input[y][x];
        east += InRange(y, x + 1, input);
        east += InRange(y, x + 2, input);
        east += InRange(y, x + 3, input);
        return east == "XMAS" ? 1 : 0;
    }

    private static int GetWordsSE(int y, int x, List<string> input)
    {
        var southeast = "";
        southeast += input[y][x];
        southeast += InRange(y + 1, x + 1, input);
        southeast += InRange(y + 2, x + 2, input);
        southeast += InRange(y + 3, x + 3, input);
        return southeast == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsS(int y, int x, List<string> input)
    {
        var south = "";
        south += input[y][x];
        south += InRange(y + 1, x, input);
        south += InRange(y + 2, x, input);
        south += InRange(y + 3, x, input);
        return south == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsSW(int y, int x, List<string> input)
    {
        var southwest = "";
        southwest += input[y][x];
        southwest += InRange(y + 1, x - 1, input);
        southwest += InRange(y + 2, x - 2, input);
        southwest += InRange(y + 3, x - 3, input);
        return southwest == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsW(int y, int x, List<string> input)
    {
        var west = "";
        west += input[y][x];
        west += InRange(y, x - 1, input);
        west += InRange(y, x - 2, input);
        west += InRange(y, x - 3, input);
        return west == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsNW(int y, int x, List<string> input)
    {
        var northwest = "";
        northwest += input[y][x];
        northwest += InRange(y - 1, x - 1, input);
        northwest += InRange(y - 2, x - 2, input);
        northwest += InRange(y - 3, x - 3, input);
        return northwest == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsN(int y, int x, List<string> input)
    {
        var north = "";
        north += input[y][x];
        north += InRange(y - 1, x, input);
        north += InRange(y - 2, x, input);
        north += InRange(y - 3, x, input);
        return north == "XMAS" ? 1 : 0;;
    }

    private static int GetWordsNE(int y, int x, List<string> input)
    {
        var northeast = "";
        northeast += input[y][x];
        northeast += InRange(y - 1, x + 1, input);
        northeast += InRange(y - 2, x + 2, input);
        northeast += InRange(y - 3, x + 3, input);
        return northeast == "XMAS" ? 1 : 0;;
    }

    private static char InRange(int y, int x, List<string> input)
    {
        if (y >= 0 && y < input.Count)
        {
            if (x >= 0 && x < input[y].Length)
            {
                return input[y][x];
            }

            return '.';
        }

        return '.';
    }

    private static int IsXMas(int y, int x, List<string> input)
    {
        if (y >= 0 && y < input.Count)
        {
            if (x >= 0 && x < input[y].Length)
            {
                if (input[y][x] == 'A')
                {
                    var mas1 = "";
                    mas1 += InRange(y - 1, x - 1, input);
                    mas1 += input[y][x];
                    mas1 += InRange(y + 1, x + 1, input);
                    var mas2 = "";
                    mas2 += InRange(y + 1, x - 1, input);
                    mas2 += input[y][x];
                    mas2 += InRange(y - 1, x + 1, input);
                    return (mas1 == "SAM" || mas1 == "MAS")
                           && (mas2 == "SAM" || mas2 == "MAS")
                        ? 1
                        : 0;
                }
            }
        }

        return 0;
    }
}