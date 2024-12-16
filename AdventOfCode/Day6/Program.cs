using Day6;

const string dataFile = "../../../data/data.txt";

var input = File.ReadAllLines(dataFile);

var mapSize = new Size(input.Length, input[0].Length);

var map = new Map(mapSize);

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


var mainGuard = new Guard(guardPosition, mapSize);
mainGuard.MarkCurrentAsVisited();

var additionalObstructions = 0;

var alreadyObstructed = new bool [input.Length, input[0].Length];

void CheckForAlternativePaths()
{
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

            if (testGuard.HasAlreadyVisitedCurrent())
            {
                additionalObstructions++;
                break;
            }

            testGuard.MarkCurrentAsVisited();

            if (
                mainGuard.HasVisited(testGuard.CurrentPosition, testGuard.CurrentDirection)
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
        mainGuard.MoveAndVisit();
    }
}


Console.WriteLine($"Total fields guard visited: {mainGuard.GetVisitedPositionsAmount()}");
Console.WriteLine($"Total additional obstruction positions: {additionalObstructions}");