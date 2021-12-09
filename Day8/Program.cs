Part1();
Part2();

void Part1()
{
    bool IsEasyDigit(int digit) => digit == 1 || digit == 4 || digit == 7 || digit == 8;

    var displayEntries = FetchDisplayEntries();
    var outputValueSum = 0;
    foreach (var entry in displayEntries)
    {
        var digitMapper = GetDictionaryMapper(entry.SignalPatterns);
        outputValueSum += entry.DigitOuputs.Where(d => IsEasyDigit(digitMapper[d])).Count();
    }

    Console.WriteLine("Part 1: {0}", outputValueSum);
}

void Part2()
{
    var displayEntries = FetchDisplayEntries();
    var outputValueSum = 0;
    foreach (var entry in displayEntries)
    {
        var digitMapper = GetDictionaryMapper(entry.SignalPatterns);
        var digitStr = string.Join("", entry.DigitOuputs.Select(o => digitMapper[o].ToString()));
        outputValueSum += int.Parse(digitStr);
    }
    Console.WriteLine("Part 2: {0}", outputValueSum);
}

Dictionary<string, int> GetDictionaryMapper(string[] signalPatterns)
{
    var patternsPerCharLength = signalPatterns.GroupBy(x => x.Length).ToDictionary(grp => grp.Key, grp => grp.ToList());
    var one = patternsPerCharLength[2][0];
    var seven = patternsPerCharLength[3][0];
    var four = patternsPerCharLength[4][0];
    var eight = patternsPerCharLength[7][0];
   
    var twoThreeFive = patternsPerCharLength[5];
    var zeroNineSix = patternsPerCharLength[6];

    var six = zeroNineSix.First(x => !one.All(x.Contains));
    var zeroNine = zeroNineSix.Where(x => x != six);

    var three = twoThreeFive.First(x => one.All(x.Contains));
    var twoFive = twoThreeFive.Where(x => x != three);

    var nine = zeroNine.First(x => three.All(x.Contains));
    var zero = zeroNine.First(x => x != nine);

    var five = twoFive.First(x => x.All(nine.Contains));
    var two = twoFive.First(x => x != five);

    return new Dictionary<string, int>
    {
        { zero, 0 },
        { one, 1 },
        { two, 2 },
        { three, 3 },
        { four, 4 },
        { five, 5 },
        { six, 6 },
        { seven, 7 },
        { eight, 8 },
        { nine, 9 }
    };
}

string[] FetchInput() => File.ReadAllLines("input.txt");
DisplayEntry[] FetchDisplayEntries()
{
    string SortString(string input)
    {
        var characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }
    
    string[] PrepareInputArrays(string input)
    {
        return input.Trim().Split(" ").Select(SortString).ToArray();
    }
    
    return FetchInput().Select(s =>
    {
        var split = s.Split("|");
        return new DisplayEntry(PrepareInputArrays(split[0]), PrepareInputArrays(split[1]));
    }).ToArray();
}

public record DisplayEntry(string[] SignalPatterns, string[] DigitOuputs);