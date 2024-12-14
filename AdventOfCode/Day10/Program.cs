const string dataFile = "../../../data/data.txt";

var lines = File.ReadAllLines(dataFile);
var trails = lines.Select(line => line.Select(x => x - '0').ToArray()).ToArray();

var rowsCount = trails.Length;
var columnsCount = trails[0].Length;

var totalScore = 0;
var totalRating = 0;

bool IsWithinBounds(int row, int column)
{
    return row >= 0 && row < rowsCount && column >= 0 && column < columnsCount;
}


bool AreDirectionsDiagonal(int i, int columnDirection1)
{
    return Math.Abs(i + columnDirection1) != 1;
}

void GetPathScore(int row, int column, List<(int, int)> trailEndings, int height = 0)
{
    if (!IsWithinBounds(row, column)) return;
    if (trails[row][column] != height) return;
    if (trails[row][column] == height && height == 9)
    {
        trailEndings.Add((row, column));
        return;
    }

    for (var rowDirection = -1; rowDirection <= 1; rowDirection++)
    for (var columnDirection = -1; columnDirection <= 1; columnDirection++)
    {
        if (rowDirection == 0 && columnDirection == 0) continue;
        if (AreDirectionsDiagonal(rowDirection, columnDirection)) continue;

        GetPathScore(row + rowDirection, column + columnDirection, trailEndings, height + 1);
    }
}

int GetTrailHeadScore(int row, int column)
{
    var trailEndings = new List<(int, int)>();
    GetPathScore(row, column, trailEndings);
    return trailEndings.Distinct().ToArray().Length;
}

int GetTrailHeadRating(int row, int column)
{
    var trailEndings = new List<(int, int)>();
    GetPathScore(row, column, trailEndings);
    return trailEndings.Count;
}


for (var row = 0; row < trails.Length; row++)
for (var column = 0; column < trails[row].Length; column++)
{
    totalScore += GetTrailHeadScore(row, column);
    totalRating += GetTrailHeadRating(row, column);
}

Console.WriteLine($"Total score: {totalScore}");
Console.WriteLine($"Total rating: {totalRating}");