const string dataFile = "../../../data/data.txt";

List<List<bool>> obstructions = [];

List<List<bool>> visited = [];
var totalVisited = 0;

var guardRow = 0;
var guardColumn = 0;
var guardDirection = GuardDirection.Up;


bool hasGuardReachedBorder()
{
    return guardRow == 0 ||
           guardColumn == 0 ||
           guardRow == obstructions.Count - 1 ||
           guardColumn == obstructions[0].Count - 1;
}

bool isBorder(int x, int y)
{
    return x == 0 ||
           y == 0 ||
           x == obstructions.Count - 1 ||
           y == obstructions[0].Count - 1;
}


var currentRow = 0;

foreach (var line in File.ReadLines(dataFile))
{
    obstructions.Add([]);
    visited.Add([]);

    var currentColumn = 0;
    foreach (var location in line.ToCharArray())
    {
        if (location == '^')
        {
            guardRow = currentRow;
            guardColumn = currentColumn;
            visited[currentRow].Add(true);
            totalVisited++;
        }
        else
        {
            visited[currentRow].Add(false);
        }

        obstructions[currentRow].Add(location == '#');


        currentColumn++;
    }

    currentRow++;
}

(int, int) GetNewGuardPosition()
{
    return guardDirection switch
    {
        GuardDirection.Up => (-1, 0),
        GuardDirection.Down => (1, 0),
        GuardDirection.Left => (0, -1),
        GuardDirection.Right => (0, 1),
        _ => throw new ArgumentOutOfRangeException()
    };
}

void changeDirection()
{
    guardDirection = guardDirection switch
    {
        GuardDirection.Up => GuardDirection.Right,
        GuardDirection.Down => GuardDirection.Left,
        GuardDirection.Left => GuardDirection.Up,
        GuardDirection.Right => GuardDirection.Down,
        _ => throw new ArgumentOutOfRangeException()
    };
}

var test = 0;

while (!hasGuardReachedBorder())
{
    var newPosition = GetNewGuardPosition();

    if (obstructions[guardRow + newPosition.Item1][guardColumn + newPosition.Item2])
    {
        changeDirection();
    }
    else
    {
        guardRow += newPosition.Item1;
        guardColumn += newPosition.Item2;
        if (!visited[guardRow][guardColumn])
        {
            visited[guardRow][guardColumn] = true;
            totalVisited++;
        }
    }
}

Console.WriteLine($"Total fields guard visited: {totalVisited}");
Console.WriteLine($"Total fields guard visited: {test}");

internal enum GuardDirection
{
    Up,
    Down,
    Left,
    Right
}

// 5030