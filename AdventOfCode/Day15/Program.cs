using Day15;

const string dataFile = "../../../data/data.txt";
var playerPosition = new Position(0, 0);

List<char[]> map = [];
List<char> commands = [];
const char Player = '@';
const char Wall = '#';
const char Box = 'O';
const char Empty = '.';

foreach (var line in File.ReadLines(dataFile))
{
    if (line.StartsWith(Wall))
    {
        map.Add(line.ToCharArray());
        continue;
    }

    if (line.Length == 0) continue;

    commands.AddRange(line.ToCharArray());
}

var mapSizeX = map[0].Length;
var mapSizeY = map.Count;

char GetObjectAtPosition(Position position)
{
    return map[position.Y][position.X];
}

void SetObjectAtPosition(Position position, char obj)
{
    map[position.Y][position.X] = obj;
}

for (var x = 0; x < mapSizeX; x++)
{
    for (var y = 0; y < mapSizeY; y++)
    {
        var currPosition = new Position(x, y);
        if (GetObjectAtPosition(currPosition) == Player)
        {
            playerPosition = currPosition;
        }
    }
}

Dictionary<char, Transition> directions = new()
{
    { '^', new Transition(0, -1) },
    { '>', new Transition(1, 0) },
    { '<', new Transition(-1, 0) },
    { 'v', new Transition(0, 1) }
};

foreach (var command in commands)
{
    var transition = directions[command];
    var newPosition = transition.TransitionPosition(playerPosition);

    if (GetObjectAtPosition(newPosition) == Wall)
    {
        continue;
    }

    if (GetObjectAtPosition(newPosition) == Empty)
    {
        SetObjectAtPosition(playerPosition, Empty);
        SetObjectAtPosition(newPosition, Player);
        playerPosition = newPosition;
    }

    if (GetObjectAtPosition(newPosition) == Box)
    {
        var position = newPosition.Copy();

        while (GetObjectAtPosition(position) == Box)
        {
            position = transition.TransitionPosition(position);
        }

        if (GetObjectAtPosition(position) == Wall) continue;

        SetObjectAtPosition(position, Box);
        SetObjectAtPosition(playerPosition, Empty);
        SetObjectAtPosition(newPosition, Player);
        playerPosition = newPosition;
    }
}

var sum = 0L;

const int YCoordinateMultiplier = 100;
const int XCoordinateMultiplier = 1;

for (var x = 0; x < mapSizeX; x++)
{
    for (var y = 0; y < mapSizeY; y++)
    {
        if (GetObjectAtPosition(new Position(x, y)) == Box)
        {
            sum += y * YCoordinateMultiplier + x * XCoordinateMultiplier;
        }
    }
}

Console.WriteLine($"Sum of coordinates: {sum}");


const char LeftBox = '[';
const char RightBox = ']';

map = [];
foreach (var line in File.ReadLines(dataFile))
{
    if (line.StartsWith(Wall))
    {
        map.Add(line.ToCharArray());
        continue;
    }

    if (line.Length == 0) break;

    // commands.AddRange(line.ToCharArray());
}

List<List<char>> extendedMap = [];

char GetObjectAtPosition2(Position position)
{
    return extendedMap[position.Y][position.X];
}

void SetObjectAtPosition2(Position position, char obj)
{
    extendedMap[position.Y][position.X] = obj;
}


for (var i = 0; i < mapSizeY; i++)
{
    extendedMap.Add([]);
    for (var j = 0; j < mapSizeX; j++)
    {
        if (map[i][j] == Box)
        {
            extendedMap[i].AddRange([LeftBox, RightBox]);
            continue;
        }

        if (map[i][j] == Player)
        {
            extendedMap[i].AddRange([Player, Empty]);
            continue;
        }

        extendedMap[i].AddRange([map[i][j], map[i][j]]);
    }
}

for (var row = 0; row < extendedMap.Count; row++)
{
    for (var col = 0; col < extendedMap[0].Count; col++)
    {
        var currPosition = new Position(col, row);
        if (GetObjectAtPosition2(currPosition) == Player)
        {
            playerPosition = currPosition;
        }
    }
}

void DrawMap()
{
    // foreach (var row in extendedMap)
    // {
    //     foreach (var col in row)
    //     {
    //         Console.Write(col);
    //     }
    //
    //     Console.Write('\n');
    // }
}


bool CanMoveBoxes(Position position, Transition transition)
{
    var newPosition = transition.TransitionPosition(position);

    if (GetObjectAtPosition2(newPosition) == Wall)
    {
        return false;
    }

    var x = true;

    if (GetObjectAtPosition2(newPosition) is LeftBox or RightBox)
    {
        x = CanMoveBoxes(newPosition, transition);
    }

    var secondPosition = directions['>'].TransitionPosition(position);
    if (GetObjectAtPosition2(position) == RightBox)
    {
        secondPosition = directions['<'].TransitionPosition(position);
    }

    var newSecondPosition = transition.TransitionPosition(secondPosition);

    if (GetObjectAtPosition2(newSecondPosition) is LeftBox or RightBox)
    {
        return CanMoveBoxes(newSecondPosition, transition) && x;
    }

    if (GetObjectAtPosition2(newSecondPosition) == Wall)
    {
        return false;
    }

    return x;
}

void MoveBoxes(Position position, Transition transition, int depth = 0)
{
    var newPosition = transition.TransitionPosition(position);
    var secondPosition = directions['>'].TransitionPosition(position);
    if (GetObjectAtPosition2(position) == RightBox)
    {
        secondPosition = directions['<'].TransitionPosition(position);
    }

    var newSecondPosition = transition.TransitionPosition(secondPosition);


    if (GetObjectAtPosition2(newPosition) != Empty)
    {
        MoveBoxes(newPosition, transition, depth + 1);
    }

    if (GetObjectAtPosition2(newSecondPosition) != Empty)
    {
        MoveBoxes(newSecondPosition, transition, depth + 1);
    }

    SetObjectAtPosition2(newPosition, GetObjectAtPosition2(position));
    SetObjectAtPosition2(newSecondPosition, GetObjectAtPosition2(secondPosition));
    SetObjectAtPosition2(position, Empty);
    SetObjectAtPosition2(secondPosition, Empty);
}

void MoveBoxesVertically(Position position, Transition transition)
{
    var newPosition = transition.TransitionPosition(position);
    if (GetObjectAtPosition2(newPosition) is LeftBox or RightBox)
    {
        MoveBoxesVertically(newPosition, transition);
    }

    SetObjectAtPosition2(newPosition, GetObjectAtPosition2(position));
}

bool CanMoveBoxesVertically(Position position, Transition transition)
{
    var newPosition = position;
    while (GetObjectAtPosition2(newPosition) is RightBox or LeftBox)
    {
        newPosition = transition.TransitionPosition(newPosition);
    }

    return GetObjectAtPosition2(newPosition) == Empty;
}

DrawMap();
foreach (var command in commands)
{
    var transition = directions[command];
    var newPosition = transition.TransitionPosition(playerPosition);

    if (GetObjectAtPosition2(newPosition) == Wall)
    {
        DrawMap();
        continue;
    }

    if (GetObjectAtPosition2(newPosition) == Empty)
    {
        SetObjectAtPosition2(playerPosition, Empty);
        SetObjectAtPosition2(newPosition, Player);
        playerPosition = newPosition;
        DrawMap();
        continue;
    }

    if (command is '<' or '>')
    {
        if (CanMoveBoxesVertically(newPosition, transition))
        {
            MoveBoxesVertically(newPosition, transition);
            SetObjectAtPosition2(newPosition, Player);
            SetObjectAtPosition2(playerPosition, Empty);
            playerPosition = newPosition;
        }

        DrawMap();
        continue;
    }


    if (CanMoveBoxes(newPosition, transition))
    {
        MoveBoxes(newPosition, transition);

        SetObjectAtPosition2(newPosition, Player);
        SetObjectAtPosition2(playerPosition, Empty);
        playerPosition = newPosition;
    }

    DrawMap();
}

sum = 0L;

for (var row = 0; row < extendedMap.Count; row++)
{
    for (var col = 0; col < extendedMap[0].Count; col++)
    {
        var currPosition = new Position(col, row);
        if (GetObjectAtPosition2(currPosition) == LeftBox)
        {
            sum += currPosition.Y * YCoordinateMultiplier + currPosition.X * XCoordinateMultiplier;
        }
    }
}

Console.WriteLine($"Sum of coordinates: {sum}");