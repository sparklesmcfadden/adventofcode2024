namespace AdventOfCode2024;

public static class Day9
{
    public static long Part1(string inputPath)
    {
        var input = Utilities.LoadFile(inputPath);
        var blockList = MapToBlocks(input);

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
        var input = Utilities.LoadFile(inputPath);
        var blockList = MapToBlockFiles(input);

        for (var i = blockList.Count - 1; i >= 0; i--)
        {
            var fileToMove = blockList[i];
            if (fileToMove.FileId == -1) continue;
            for (var j = 0; j < i; j++)
            {
                if (blockList[j].FileId == -1) // it is free space
                {
                    var blankLength = blockList[j].Length;
                    if (blankLength >= fileToMove.Length)
                    {
                        blockList[j] = fileToMove;
                        blockList[i] = new BlockFile(-1, fileToMove.Length);
                        if (blankLength > fileToMove.Length)
                        {
                            blockList.Insert(j + 1, new BlockFile(-1, blankLength - fileToMove.Length));
                        }

                        break;
                    }
                }
            }
        }

        var blocks = BlockFilesToBlocks(blockList);
        return CalculateChecksum2(blocks);
    }

    private static List<BlockFile> MapToBlockFiles(string diskMap)
    {
        var fileId = 0;
        var blockList = new List<BlockFile>();
        for (var i = 0; i < diskMap.Length; i++)
        {
            var counter = Int32.Parse(diskMap[i].ToString());
            if (i % 2 == 0)
            {
                blockList.Add(new BlockFile(fileId, counter));
                fileId++;
            }

            if (i % 2 != 0)
            {
                blockList.Add(new BlockFile(-1, counter));
            }
        }

        return blockList;
    }

    private static List<int> BlockFilesToBlocks(List<BlockFile> blockFiles)
    {
        var blockList = new List<int>();
        foreach (var blockFile in blockFiles)
        {
            blockList.AddRange(Enumerable.Repeat(blockFile.FileId, blockFile.Length));
        }

        return blockList;
    }

    private static List<int> MapToBlocks(string diskMap)
    {
        var fileId = 0;
        var blockList = new List<int>();
        for (var i = 0; i < diskMap.Length; i++)
        {
            var counter = Int32.Parse(diskMap[i].ToString());
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

        return blockList;
    }

    private static long CalculateChecksum(List<int> fileBlocks)
    {
        return fileBlocks.TakeWhile(t => t >= 0).Select(Convert.ToInt64).Select((t, i) => i * t).Sum();
    }

    private static long CalculateChecksum2(List<int> fileBlocks)
    {
        long checksum = 0;
        for (var i = 0; i < fileBlocks.Count; i++)
        {
            if (fileBlocks[i] > 0)
            {
                checksum += fileBlocks[i] * i;
            }
        }

        return checksum;
    }

    private class BlockFile(int fileId, int length)
    {
        public int FileId { get; set; } = fileId;
        public int Length { get; set; } = length;
    }
}