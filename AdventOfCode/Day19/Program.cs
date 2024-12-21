List<string> availableTowels = [];

const string dataFile = "../../../data/data.txt";
var input = File.ReadAllLines(dataFile);


foreach (var towel in input[0].Split(", ").ToArray())
    availableTowels.Add(towel);

var patterns = input[2..];

bool IsPossible(string pattern)
{
    if (pattern.Length == 0) return true;

    foreach (var towel in availableTowels)
        if (pattern.EndsWith(towel))
        {
            var trimmedPattern = pattern[..^towel.Length];
            if (IsPossible(trimmedPattern)) return true;
        }

    return false;
}

var total = 0;

foreach (var pattern in patterns)
    if (IsPossible(pattern))
        total++;

Console.WriteLine(total);