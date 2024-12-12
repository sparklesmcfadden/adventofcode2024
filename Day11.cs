using System.Transactions;

namespace AdventOfCode2024;

public static class Day11
{

    public static int Part1(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
        var stones = FileToStones(input);
        var blinks = 25;

        for (int b = 0; b < blinks; b++)
        {
            var newStones = new List<Stone>();
            foreach (var stone in stones)
            {
                newStones.AddRange(Blink(stone));
            }

            stones = newStones;
        }

        return stones.Count;
    }

    public static long SlowPart2(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
        var stones = FileToStone2s(input);
        var blinks = 75;

        var count = SlowBlink(stones, blinks);

        return count;
    }

    public static long Part2(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
        var stoneDict = FileToDict(input);
        var blinks = 75;

        var count = FastBlink(stoneDict, blinks);

        return count;
    }

    public static long FastBlink(Dictionary<long, long> stones, int blinks)
    {
        for (var i = 0; i < blinks; i++)
        {
            var deltas = new List<List<long>>();
            foreach (var stone in stones)
            {
                if (stone.Key == 0)
                {
                    deltas.Add([1, stone.Value]);
                }
                else if (stone.Key.ToString().Length % 2 == 0)
                {
                    var stoneString = stone.Key.ToString();
                    var leftStone = long.Parse(stoneString[..(stoneString.Length / 2)]);
                    var rightStone = long.Parse(stoneString[(stoneString.Length / 2)..]);
                    deltas.Add([leftStone, stone.Value]);
                    deltas.Add([rightStone, stone.Value]);
                }
                else
                {
                    deltas.Add([stone.Key * 2024, stone.Value]);
                }
                deltas.Add([stone.Key, stone.Value * -1]);
            }

            foreach (var delta in deltas)
            {
                if (!stones.TryAdd(delta[0], delta[1]))
                {
                    stones[delta[0]] += delta[1];
                }
            }
        }

        return stones.Select(s => s.Value).Sum();
    }

    public static long SlowBlink(List<Stone2> stones, int blinks)
    {
        for (var i = 0; i < blinks; i++)
        {
            var deltas = new List<List<long>>();
            foreach (var stone in stones)
            {
                if (stone.Label == 0)
                {
                    deltas.Add([1, stone.StoneCount]);
                }
                else if (stone.Label.ToString().Length % 2 == 0)
                {
                    var stoneString = stone.Label.ToString();
                    var leftStone = long.Parse(stoneString[..(stoneString.Length / 2)]);
                    var rightStone = long.Parse(stoneString[(stoneString.Length / 2)..]);
                    deltas.Add([leftStone, stone.StoneCount]);
                    deltas.Add([rightStone, stone.StoneCount]);
                }
                else
                {
                    deltas.Add([stone.Label * 2024, stone.StoneCount]);
                }
                deltas.Add([stone.Label, stone.StoneCount * -1]);
            }

            foreach (var delta in deltas)
            {
                var stoneToUpdate = stones.FirstOrDefault(s => s.Label == delta[0]);
                if (stoneToUpdate != null)
                {
                    stoneToUpdate.AddStone(delta[1]);
                }
                else
                {
                    stones.Add(new Stone2(delta[0], delta[1]));
                }
            }
        }

        return stones.Select(s => s.StoneCount).Sum();
    }

    private static Dictionary<long, long> FileToDict(string file)
    {
        var dict = new Dictionary<long, long>();
        foreach (var item in file.Split(" "))
        {
            if (!dict.TryAdd(long.Parse(item), 1L))
            {
                dict[long.Parse(item)]++;
            }
        }

        return dict;
    }
    
    private static List<Stone> FileToStones(string file)
    {
        return file.Split(" ").Select(n => new Stone(n)).ToList();
    }

    private static List<Stone2> FileToStone2s(string file)
    {
        return file.Split(" ").Select(n => new Stone2(long.Parse(n), 1)).ToList();
    }
    
    public static List<Stone> Blink(Stone stone)
    {
        var stoneList = new List<Stone>();
        if (stone.Label.Length % 2 == 0)
        {
            var labels = stone.SplitLabel();
            labels.ForEach(s => stoneList.Add(new Stone(s)));
        }
        else if (stone.GetLabelInt() == 0)
        {
            const string label = "1";
            stoneList.Add(new Stone(label));
        }
        else
        {
            var label = stone.GetLabelInt() * 2024;
            stoneList.Add(new Stone(label));
        }

        return stoneList;
    }

    public class Stone2(long label, long count)
    {
        public long Label { get; set; } = label;
        public long StoneCount { get; set; } = count;

        public void AddStone(long count)
        {
            StoneCount += count;
        }

        public void RemoveStone()
        {
            StoneCount--;
        }
    }
    
    public class Stone
    {
        public Stone(string label)
        {
            Label = long.Parse(label).ToString();
        }
        
        public Stone(long label)
        {
            Label = label.ToString();
        }

        public long GetLabelInt()
        {
            return long.Parse(Label);
        }

        public List<string> SplitLabel()
        {
            var label1 = Label.Substring(0, Label.Length / 2);
            var label2 = Label.Substring(Label.Length / 2);
            return [label1, label2];
        }
        
        public string Label { get; set; }
    }
}