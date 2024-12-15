using Day6;

const string dataFile = "../../../data/data.txt";

var input = File.ReadAllLines(dataFile);

var map = new Map(input.Length, input[0].Length);

var currentRow = 0;

var playerPosition = new Position(0, 0);

foreach (var line in input)
{
    var currentColumn = 0;
    foreach (var location in line.ToCharArray())
    {
        var currentPosition = new Position(currentColumn, currentRow);
        switch (location)
        {
            case '^':
                playerPosition = currentPosition;
                map.Visit(currentPosition, Direction.Up);
                break;
            case '#':
                map.AddObstruction(currentPosition);
                break;
        }

        currentColumn++;
    }

    currentRow++;
}

var mainPlayer = new Player(playerPosition);
var additionalObstructions = 0;


var obs = new bool [input.Length, input[0].Length];

void CheckForAlternativePaths()
{
    var visitDirection = new Direction [input.Length, input[0].Length];
    var visited = new bool [input.Length, input[0].Length];
    var newPlayer = mainPlayer.Copy();
    var positionToObstruct = newPlayer.NextPosition();

    if (map.HasObstruction(positionToObstruct)) return;
    if (obs[positionToObstruct.X, positionToObstruct.Y]) return;
    obs[positionToObstruct.X, positionToObstruct.Y] = true;

    map.AddObstruction(positionToObstruct);
    while (!map.IsPositionOnBorder(newPlayer.CurrentPosition))
    {
        var nextPosition = newPlayer.NextPosition();

        if (map.HasObstruction(nextPosition))
        {
            newPlayer.ChangeDirection();
        }
        else
        {
            newPlayer.Move();

            if (visited[newPlayer.CurrentPosition.X, newPlayer.CurrentPosition.Y] &&
                visitDirection[newPlayer.CurrentPosition.X, newPlayer.CurrentPosition.Y] == newPlayer.CurrentDirection)
            {
                additionalObstructions++;
                break;
            }

            visited[newPlayer.CurrentPosition.X, newPlayer.CurrentPosition.Y] = true;
            visitDirection[newPlayer.CurrentPosition.X, newPlayer.CurrentPosition.Y] = newPlayer.CurrentDirection;
            if (
                map.IsVisited(newPlayer.CurrentPosition) &&
                map.GetVisitDirection(newPlayer.CurrentPosition) == newPlayer.CurrentDirection
            )
            {
                additionalObstructions++;
                break;
            }
        }
    }

    map.RemoveObstruction(positionToObstruct);
}


while (!map.IsPositionOnBorder(mainPlayer.CurrentPosition))
{
    var nextPosition = mainPlayer.NextPosition();

    if (map.HasObstruction(nextPosition))
    {
        mainPlayer.ChangeDirection();
    }
    else
    {
        CheckForAlternativePaths();
        mainPlayer.Move();
        map.Visit(mainPlayer.CurrentPosition, mainPlayer.CurrentDirection);
    }
}


Console.WriteLine($"Total fields guard visited: {map.TotalVisited}");
Console.WriteLine($"Total additional obstruction positions: {additionalObstructions}");