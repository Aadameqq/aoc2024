using Day15;
using Day8;

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