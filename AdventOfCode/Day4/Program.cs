const string dataFile = "./data/data.txt";

var data = File.ReadAllLines(dataFile).ToList();
var rowsAmount = data[0].Length;
var columnsAmount = data.Count;


bool IsWithinBounds(int rowIndex, int columnIndex)
{
    return rowIndex >= 0 && rowIndex < rowsAmount &&
           columnIndex >= 0 && columnIndex < columnsAmount;
}

var total = 0;

for (var row = 0; row < rowsAmount; row++)
for (var column = 0; column < columnsAmount; column++)
{
    if (data[row][column] != 'X') continue;

    for (var rowDirection = -1; rowDirection <= 1; rowDirection++)
    for (var columnDirection = -1; columnDirection <= 1; columnDirection++)
    {
        if (rowDirection == 0 && columnDirection == 0) continue;

        if (!IsWithinBounds(row + rowDirection * 3, column + columnDirection * 3)) continue;

        var mLetterCandidate = data[row + rowDirection][column + columnDirection];
        var aLetterCandidate = data[row + rowDirection * 2][column + columnDirection * 2];
        var sLetterCandidate = data[row + rowDirection * 3][column + columnDirection * 3];

        if (mLetterCandidate == 'M' &&
            aLetterCandidate == 'A' &&
            sLetterCandidate == 'S')
            total++;
    }
}

var totalOfXShape = 0;

bool IsSearchedSequence(char a, char b)
{
    return (a == 'M' && b == 'S') || (a == 'S' && b == 'M');
}

for (var row = 0; row < rowsAmount; row++)
for (var column = 0; column < columnsAmount; column++)
{
    if (data[row][column] != 'A') continue;

    var rowBehind = row - 1;
    var columnBehind = column - 1;
    var rowNext = row + 1;
    var columnNext = column + 1;

    if (!IsWithinBounds(rowBehind, columnBehind) ||
        !IsWithinBounds(rowNext, columnNext)) continue;

    if (IsSearchedSequence(data[rowBehind][columnBehind], data[rowNext][columnNext]) &&
        IsSearchedSequence(data[rowBehind][columnNext], data[rowNext][columnBehind]))
        totalOfXShape++;
}

Console.WriteLine($"Found: {total}");
Console.WriteLine($"Found x shape: {totalOfXShape}");