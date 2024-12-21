List<string> availableTowels = [];

const string dataFile = "../../../data/data.txt";
var input = File.ReadAllLines(dataFile);


foreach (var towel in input[0].Split(", ").ToArray())
    availableTowels.Add(towel);

var patterns = input[2..];

var patternsCache = new Dictionary<string, int>();

int CountPossible(string pattern, string current)
{
    if (pattern.Length == 0) return 1;


    if (patternsCache.TryGetValue(pattern, out var count)) return count;

    var possible = 0;

    foreach (var towel in availableTowels)
        if (pattern.EndsWith(towel))
        {
            var trimmedPattern = pattern[..^towel.Length];
            possible += CountPossible(trimmedPattern, pattern[^towel.Length..] + current);
        }

    patternsCache.TryAdd(current, possible);
    return possible;
}

var total = 0;

foreach (var pattern in patterns)
{
    var possible = CountPossible(pattern, "");
    patternsCache.Add(pattern, possible);
    if (possible > 0) total++;
}

Console.WriteLine(total);