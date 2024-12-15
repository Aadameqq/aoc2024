using Day6;

const string dataFile = "../../../data/data.txt";

var input = File.ReadAllLines(dataFile);

var map = new Map(input.Length, input[0].Length);

var currentRow = 0;

var guardPosition = new Position(0, 0);

foreach (var line in input)
{
    var currentColumn = 0;
    foreach (var location in line.ToCharArray())
    {
        var currentPosition = new Position(currentColumn, currentRow);
        switch (location)
        {
            case '^':
                guardPosition = currentPosition;

                break;
            case '#':
                map.AddObstruction(currentPosition);
                break;
        }

        currentColumn++;
    }

    currentRow++;
}

var mainGuard = new Guard(guardPosition);
map.Visit(mainGuard.CurrentPosition, mainGuard.CurrentDirection);
var additionalObstructions = 0;

var alreadyObstructed = new bool [input.Length, input[0].Length];

void CheckForAlternativePaths()
{
    var testGuardVisited = new VisitedPositions(input.Length, input[0].Length);
    var testGuard = mainGuard.Copy();
    var positionToObstruct = testGuard.NextPosition();

    if (map.HasObstruction(positionToObstruct)) return;

    if (alreadyObstructed[positionToObstruct.X, positionToObstruct.Y]) return;
    alreadyObstructed[positionToObstruct.X, positionToObstruct.Y] = true;

    map.AddObstruction(positionToObstruct);
    while (!map.IsPositionOnBorder(testGuard.CurrentPosition))
    {
        var nextPosition = testGuard.NextPosition();

        if (map.HasObstruction(nextPosition))
        {
            testGuard.ChangeDirection();
        }
        else
        {
            testGuard.Move();

            if (testGuardVisited.HasBeenVisited(testGuard.CurrentPosition) &&
                testGuardVisited.GetDirection(testGuard.CurrentPosition) == testGuard.CurrentDirection)
            {
                additionalObstructions++;
                break;
            }

            testGuardVisited.MarkAsVisited(testGuard.CurrentPosition, testGuard.CurrentDirection);

            if (
                map.IsVisited(testGuard.CurrentPosition) &&
                map.GetVisitDirection(testGuard.CurrentPosition) == testGuard.CurrentDirection
            )
            {
                additionalObstructions++;
                break;
            }
        }
    }

    map.RemoveObstruction(positionToObstruct);
}


while (!map.IsPositionOnBorder(mainGuard.CurrentPosition))
{
    var nextPosition = mainGuard.NextPosition();

    if (map.HasObstruction(nextPosition))
    {
        mainGuard.ChangeDirection();
    }
    else
    {
        CheckForAlternativePaths();
        mainGuard.Move();
        map.Visit(mainGuard.CurrentPosition, mainGuard.CurrentDirection);
    }
}


Console.WriteLine($"Total fields guard visited: {map.TotalVisited}");
Console.WriteLine($"Total additional obstruction positions: {additionalObstructions}");