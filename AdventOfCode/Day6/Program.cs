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
                map.Visit(currentPosition);
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

while (!map.IsPositionOnBorder(mainPlayer.CurrentPosition))
{
    var nextPosition = mainPlayer.NextPosition();

    if (map.HasObstruction(nextPosition))
    {
        mainPlayer.ChangeDirection();
    }
    else
    {
        mainPlayer.Move();
        map.Visit(mainPlayer.CurrentPosition);
    }
}

Console.WriteLine($"Total fields guard visited: {map.TotalVisited}");

// 5030