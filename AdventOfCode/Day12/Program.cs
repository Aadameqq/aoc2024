using Day12;

const string dataFile = "../../../data/data.txt";
var fields = File.ReadAllLines(dataFile);

var totalRows = fields.Length;
var totalColumns = fields.First().Length;

var alreadyChecked = new bool[totalRows, totalColumns];

bool IsWithinBounds(int row, int column)
{
    return row >= 0 && row < totalRows && column >= 0 && column < totalColumns;
}

List<(int, int)> directionsTransitions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

Plot FindPlotRecursively(int row, int column)
{
    var plot = new Plot();

    alreadyChecked[row, column] = true;

    foreach (var directionTransition in directionsTransitions)
    {
        var (rowTransition, columnTransition) = directionTransition;
        var nextRow = row + rowTransition;
        var nextColumn = column + columnTransition;

        if (!IsWithinBounds(nextRow, nextColumn)) continue;
        if (fields[nextRow][nextColumn] != fields[row][column]) continue;

        plot.DecreasePerimeter();

        if (alreadyChecked[nextRow, nextColumn]) continue;

        var nextPlot = FindPlotRecursively(nextRow, nextColumn);
        plot.MergeWith(nextPlot);
    }

    return plot;
}

var totalCost = 0;

for (var row = 0; row < totalRows; row++)
for (var column = 0; column < totalColumns; column++)
    if (!alreadyChecked[row, column])
        totalCost += FindPlotRecursively(row, column).CalculateCost();

Console.WriteLine($"Total cost: {totalCost}");