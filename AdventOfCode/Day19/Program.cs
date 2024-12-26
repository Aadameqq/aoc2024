List<string> availableTowels = [];

const string dataFile = "./data/data.txt";
var input = File.ReadAllLines(dataFile);

foreach (var towel in input[0].Split(", ").ToArray())
    availableTowels.Add(towel);

var patterns = input[2..];

var patternsCache = new Dictionary<string, long>();

long CountPossible(string pattern)
{
    if (patternsCache.TryGetValue(pattern, out var count)) return count;
    if (pattern.Length == 0) return 1;

    var possible = 0L;

    foreach (var towel in availableTowels)
        if (pattern.EndsWith(towel))
        {
            var trimmedPattern = pattern[..^towel.Length];
            possible += CountPossible(trimmedPattern);
        }

    patternsCache.TryAdd(pattern, possible);

    return possible;
}

var totalPossible = 0;
var totalWays = 0L;

foreach (var pattern in patterns)
{
    var possible = CountPossible(pattern);
    totalWays += possible;
    if (possible > 0)
        totalPossible++;
}

Console.WriteLine(totalPossible);
Console.WriteLine(totalWays);