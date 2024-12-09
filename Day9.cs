namespace AdventOfCode2024;

public static class Day9
{   
    public static long Part1(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);

        var fileId = 0;
        var blockList = new List<int>();
        for (var i = 0; i < input.Length; i++)
        {
            var counter = Int32.Parse(input[i].ToString());
            if (i % 2 == 0)
            {
                blockList.AddRange(Enumerable.Repeat(fileId, counter));
                fileId++;
            }
            if (i % 2 != 0)
            {
                blockList.AddRange(Enumerable.Repeat(-1, counter));
            }
        }

        var lastBlockIndex = blockList.Count - 1;
        for (var i = 0; i < blockList.Count; i++)
        {
            if (blockList[i] == -1)
            {
                for (var j = lastBlockIndex; j > i; j--)
                {
                    if (blockList[j] > 0)
                    {
                        var lastBlock = blockList[j];
                        blockList[i] = lastBlock;
                        blockList[j] = -1;
                        lastBlockIndex = j;
                        break;
                    }
                }
            }
        }

        return CalculateChecksum(blockList);
    }
    
    public static long Part2(string inputPath)
    {
        var input = Utilities.LoadFileAsLines(inputPath);
        
        return 0;
    }

    private static long CalculateChecksum(List<int> fileBlocks)
    {
        return fileBlocks.TakeWhile(t => t >= 0).Select(Convert.ToInt64).Select((t, i) => i * t).Sum();
    }
}