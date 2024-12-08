namespace AdventOfCode2024;

public static class Day4
{
    public static int Part1(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);
        var count = 0;

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                const string word = "XMAS";
                count += input[y][x] == word[0] ? GetWords(y, x, input, word) : 0;
            }
        }

        return count;
    }

    public static int Part2(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);
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
        var directions = new List<int[]>
        {
            new[] { 0, 1 }, new[] { 1, 1 }, new[] { 1, 0 }, new[] { 1, -1 },
            new[] { 0, -1 }, new[] { -1, -1 }, new[] { -1, 0 }, new[] { -1, 1 },
        };
        foreach (var direction in directions)
        {
            var foundWord = "";
            for (var i = 0; i < word.Length; i++)
            {
                foundWord += InRange(y + i * direction[0], x + i * direction[1], input);
            }

            count += foundWord == word ? 1 : 0;
        }

        return count;
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