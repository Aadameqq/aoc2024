using Day15;
using Day8;

const string dataFile = "../../../data/data.txt";
var playerPosition = new Position(0, 0);

List<char[]> map = [];
List<char> commands = [];

foreach (var line in File.ReadLines(dataFile))
{
    if (line.StartsWith('#'))
    {
        map.Add(line.ToCharArray());
    }

    if (line.StartsWith('>') || line.StartsWith('^') || line.StartsWith('<') || line.StartsWith('v'))
    {
        commands.AddRange(line.ToCharArray());
    }
}

for (var i = 0; i < map.Count; i++)
{
    for (var j = 0; j < map[i].Length; j++)
    {
        if (map[i][j] == '@')
        {
            playerPosition = new Position(j, i);
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

char GetMapPosition(Position position)
{
    return map[position.Y][position.X];
}

void SetMapPosition(Position position, char value)
{
    map[position.Y][position.X] = value;
}


foreach (var command in commands)
{
    var transition = directions[command];
    var newPosition = transition.TransitionPosition(playerPosition);

    if (GetMapPosition(newPosition) == '#')
    {
        // PrintMap();
        continue;
    }

    if (GetMapPosition(newPosition) == '.')
    {
        SetMapPosition(playerPosition, '.');
        SetMapPosition(newPosition, '@');
        playerPosition = newPosition;
    }

    if (GetMapPosition(newPosition) == 'O')
    {
        var position = newPosition.Copy();

        while (GetMapPosition(position) == 'O')
        {
            position = transition.TransitionPosition(position);
        }

        if (GetMapPosition(position) == '.')
        {
            SetMapPosition(position, 'O');
            SetMapPosition(playerPosition, '.');
            SetMapPosition(newPosition, '@');
            playerPosition = newPosition;
        }
    }
}

var total = 0L;

for (var i = 0; i < map.Count; i++)
{
    for (var j = 0; j < map[i].Length; j++)
    {
        if (map[i][j] == 'O')
        {
            total += i * 100 + j;
        }
    }
}

Console.WriteLine(total);