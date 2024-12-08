namespace AdventOfCode2024;

public static class Day5
{
    public static int Part1(string inputPath)
    {
        var inputFile = Utilities.LoadFile(inputPath).Split("\n\n");
        var rules = inputFile[0].Split("\n").Select(n => n.Split("|").ToList()).ToList();
        var pages = inputFile[1].Split("\n").Select(n => n.Split(",").ToList()).ToList();

        return pages.Where(page => ValidateRules(page, rules)).Sum(page => Int32.Parse(page[page.Count / 2]));
    }

    public static int Part2(string inputPath)
    {
        var inputFile = Utilities.LoadFile(inputPath).Split("\n\n");
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