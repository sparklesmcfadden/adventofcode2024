using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day6
{
    private const string Day = "6";
    private const string InputPath = $"../../../inputs/day{Day}.txt";
    private const string InputPathTest = $"../../../inputs/day{Day}_test.txt";
    private static readonly Stopwatch Timer = new();

    public static void Run()
    {
        Console.WriteLine($"Day {Day}");
        Timer.Start();
        var part1 = Day6_Part1();
        Timer.Stop();
        Console.WriteLine($"{part1} ({Timer.Elapsed.TotalMilliseconds})");

        Timer.Reset();
        Timer.Start();
        var part2 = Day6_Part2();
        Timer.Stop();
        Console.WriteLine($"{part2} ({Timer.Elapsed.TotalMilliseconds})");
    }

    private static int Day6_Part1()
    {
        var input = Utilities.LoadFileAsLines(InputPath);

        int[] coordinates = { 0, 0 };

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '^')
                {
                    coordinates = new[] { x, y };
                }
            }
        }

        var count = Walk(input, coordinates, new[] { 0, -1 });

        return count;
    }

    private static int Day6_Part2()
    {
        var input = Utilities.LoadFileAsLines(InputPath);

        int[] coordinates = { 0, 0 };

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '^')
                {
                    coordinates = new[] { x, y };
                }
            }
        }

        var obstacleSpots = 0;
        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var obstacle = new[] { x, y };
                var newInput = DropAnObstacle(input, obstacle);
                obstacleSpots += WalkWithObstacle(newInput, coordinates, new[] { 0, -1 });
            }
        }
        
        return obstacleSpots;
    }

    private static List<string> DropAnObstacle(List<string> input, int[] obstacle)
    {
        var newInput = input.Select(n => n).ToList();
        var line = newInput[obstacle[1]];
        var newLine = line.Remove(obstacle[0], 1).Insert(obstacle[0], "#");
        newInput[obstacle[1]] = newLine;
        return newInput;
    }

    private static int WalkWithObstacle(List<string> input, int[] coordinates, int[] direction)
    {
        var count = 0;
        var limit = input.Count * input[0].Length;
        while (InRange(coordinates, input))
        {
            var nextCoordinates = coordinates.Zip(direction, (i, j) => i + j).ToArray();
            if (InRange(nextCoordinates, input) && input[nextCoordinates[1]][nextCoordinates[0]] == '#')
            {
                direction = TurnRight(direction);
            }
            else
            {
                count++;
                coordinates = nextCoordinates;
        
                if (count > limit)
                {
                    return 1;
                } 
            }
        }

        return 0;
    }

    private static int Walk(List<string> input, int[] coordinates, int[] direction)
    {
        var coordinateList = new List<int[]>();
        while (InRange(coordinates, input))
        {
            var nextCoordinates = coordinates.Zip(direction, (i, j) => i + j).ToArray();
            if (InRange(nextCoordinates, input) && input[nextCoordinates[1]][nextCoordinates[0]] == '#')
            {
                direction = TurnRight(direction);
            }
            else
            {
                coordinateList.Add(coordinates);
                coordinates = nextCoordinates;
            }
            // DrawMap(coordinates, input, 0);
        }
        
        var result = coordinateList.DistinctBy(x => string.Join(",", x)).ToList();
        return result.Count;
    }

    private static bool InRange(int[] coordinates, List<string> input)
    {
        return coordinates[1] >= 0 && coordinates[0] >= 0
                                   && coordinates[1] < input.Count && coordinates[0] < input[coordinates[1]].Length;
    }

    private static int[] TurnRight(int[] direction)
    {
        switch (direction)
        {
            case [1, 0]:
                return new[] { 0, 1 };
            case [0, 1]:
                return new[] { -1, 0 };
            case [-1, 0]:
                return new[] { 0, -1 };
            case [0, -1]:
                return new[] { 1, 0 };
        }

        return new[] { 0, -1 };
    }

    private static void DrawMap(int[] coordinates, List<string> input, int count)
    {
        Console.WriteLine(count);
        for (var i = 0; i < input.Count; i++)
        {
            var line = input[i];
            var charArray = line.ToArray();
            if (i == coordinates[1])
            {
                charArray[coordinates[0]] = 'X';
            }
            foreach (var item in charArray)
            {
                Console.Write(item);
            }
            Console.Write("\n");
        }
        Console.SetCursorPosition(0, Console.CursorTop - input.Count -1);
        Thread.Sleep(50);
    }
}