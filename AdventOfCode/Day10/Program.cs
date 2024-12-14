const string dataFile = "../../../data/data.txt";

var lines = File.ReadAllLines(dataFile);

var trails = lines.Select(line => line.Select(x => x - '0').ToArray()).ToArray();

var totalScore = 0;
var totalRating = 0;

bool IsWithinBounds(int row, int column)
{
    return row >= 0 && row < trails.Length && column >= 0 && column < trails[0].Length;
}

int GetPathScore(int row, int column, bool[,] visitedEnds, int height = 0, bool isRating = false)
{
    if (!IsWithinBounds(row, column)) return 0;
    if (trails[row][column] != height) return 0;
    if (trails[row][column] == height && height == 9 && (isRating || !visitedEnds[row, column]))
    {
        visitedEnds[row, column] = true;
        return 1;
    }

    var sum = 0;
    for (var rowDirection = -1; rowDirection <= 1; rowDirection++)
    for (var columnDirection = -1; columnDirection <= 1; columnDirection++)
    {
        if (rowDirection == 0 && columnDirection == 0) continue;
        if (Math.Abs(rowDirection + columnDirection) != 1) continue;

        sum += GetPathScore(row + rowDirection, column + columnDirection, visitedEnds, height + 1, isRating);
    }

    return sum;
}

int GetTrailHeadScore(int row, int column)
{
    var visitedEnds = new bool[trails.Length, trails[0].Length];

    return GetPathScore(row, column, visitedEnds);
}

int GetTrailHeadRating(int row, int column)
{
    var visitedEnds = new bool[trails.Length, trails[0].Length];

    return GetPathScore(row, column, visitedEnds, 0, true);
}


for (var row = 0; row < trails.Length; row++)
for (var column = 0; column < trails[row].Length; column++)
{
    totalScore += GetTrailHeadScore(row, column);
    totalRating += GetTrailHeadRating(row, column);
}

Console.WriteLine($"Total score: {totalScore}");
Console.WriteLine($"Is total score correct: {totalScore == 698}");
Console.WriteLine($"Total rating: {totalRating}");
Console.WriteLine($"Is total rating correct: {totalRating == 1436}");