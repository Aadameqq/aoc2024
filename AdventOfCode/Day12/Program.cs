const string dataFile = "../../../data/data.txt";
var input = File.ReadAllLines(dataFile);

var totalRows = input.Length;
var totalColumns = input.First().Length;

var alreadyCounted = new bool[totalRows, totalColumns];

bool IsWithinBounds(int row, int column)
{
    return row >= 0 && row < totalRows && column >= 0 && column < totalColumns;
}

bool AreDirectionsDiagonal(int i, int columnDirection1)
{
    return Math.Abs(i + columnDirection1) != 1;
}

(int, int) Crawler(int row, int column)
{
    if (alreadyCounted[row, column]) return (0, 0);
    alreadyCounted[row, column] = true;
    var perimeter = 4;
    var tt = 0;
    for (var rowDirection = -1; rowDirection <= 1; rowDirection++)
    for (var columnDirection = -1; columnDirection <= 1; columnDirection++)
    {
        if (rowDirection == 0 && columnDirection == 0) continue;
        if (AreDirectionsDiagonal(rowDirection, columnDirection)) continue;
        if (!IsWithinBounds(row + rowDirection, column + columnDirection)) continue;
        if (input[row + rowDirection][column + columnDirection] != input[row][column]) continue;
        perimeter--;
        var (t, p) = Crawler(row + rowDirection, column + columnDirection);
        tt += t;
        perimeter += p;
    }

    return (tt + 1, perimeter);
}

var totalCost = 0;

for (var row = 0; row < totalRows; row++)
for (var column = 0; column < totalColumns; column++)
{
    var (count, cost) = Crawler(row, column);
    totalCost += count * cost;
}

Console.WriteLine($"Total cost: {totalCost}");