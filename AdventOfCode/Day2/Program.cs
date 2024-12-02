const string dataFile = "../../../data/data.txt";

var safeReports = 0;
var safeReportsWithDampener = 0;

const int minimumSafeDifference = 1;
const int maximumSafeDifference = 3;

bool areLevelsSafe(List<int> levels)
{
    if (levels.Count < 2) return true;

    var isPreviousDecreasing = levels[0] > levels[1];
    for (var i = 0; i < levels.Count - 1; i++)
    {
        var isCurrentDecreasing = levels[i] > levels[i + 1];
        var difference = Math.Abs(levels[i] - levels[i + 1]);

        var isDifferenceSafe = difference is >= minimumSafeDifference and <= maximumSafeDifference;

        if (isCurrentDecreasing != isPreviousDecreasing || !isDifferenceSafe) return false;

        isPreviousDecreasing = isCurrentDecreasing;
    }

    return true;
}

foreach (var line in File.ReadLines(dataFile))
{
    var levels = line.Split(' ').Select(int.Parse).ToList();

    if (areLevelsSafe(levels)) safeReports++;

    for (var indexToExclude = 0; indexToExclude < levels.Count; indexToExclude++)
    {
        var levelsWithoutExcluded = levels.Where((_, index) => index != indexToExclude).ToList();

        if (areLevelsSafe(levelsWithoutExcluded))
        {
            safeReportsWithDampener++;
            break;
        }
    }
}

Console.WriteLine($"Safe reports: {safeReports}");
Console.WriteLine($"Safe reports with dampener: {safeReportsWithDampener}");