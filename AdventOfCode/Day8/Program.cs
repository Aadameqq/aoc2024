using Day8;

const string dataFile = "./data/data.txt";

var antennasByFrequency = new Dictionary<char, List<Position>>();

var antinodes = new List<List<bool>>();

var row = 0;
foreach (var line in File.ReadLines(dataFile))
{
    var column = 0;
    var currentRowAntinodes = new List<bool>();
    foreach (var location in line.ToCharArray())
    {
        if (location != '.')
            if (!antennasByFrequency.TryAdd(location, [new Position(column, row)]))
                antennasByFrequency[location].Add(new Position(column, row));

        currentRowAntinodes.Add(false);
        column++;
    }

    antinodes.Add(currentRowAntinodes);
    row++;
}

var sizeX = antinodes[0].Count;
var sizeY = row;


bool IsWithinBounds(Position position)
{
    return position.x >= 0 && position.x < sizeX && position.y >= 0 && position.y < sizeY;
}

bool TryAddAntinode(Position antinode)
{
    if (IsWithinBounds(antinode) && !antinodes[antinode.y][antinode.x])
    {
        antinodes[antinode.y][antinode.x] = true;
        return true;
    }

    return false;
}

var totalAntinodesAfterUpdate = 0;

void PutAntinodes(Position position, Transition transition)
{
    var antinodePosition = position;
    while (IsWithinBounds(antinodePosition))
    {
        if (TryAddAntinode(antinodePosition)) totalAntinodesAfterUpdate++;
        antinodePosition = transition.TransitionPosition(antinodePosition);
    }
}

var totalAntinodes = 0;

foreach (var frequency in antennasByFrequency.Keys)
{
    var antennasCount = antennasByFrequency[frequency].Count;
    for (var i = 0; i < antennasCount; i++)
    for (var j = i + 1; j < antennasCount; j++)
    {
        var firstAntenna = antennasByFrequency[frequency][i];
        var secondAntenna = antennasByFrequency[frequency][j];

        var transition = new Transition(firstAntenna, secondAntenna);

        var firstAntinode = transition.TransitionPosition(firstAntenna);
        var secondAntinode = transition.Reverse().TransitionPosition(secondAntenna);

        if (TryAddAntinode(firstAntinode))
        {
            totalAntinodes++;
            totalAntinodesAfterUpdate++;
        }

        if (TryAddAntinode(secondAntinode))
        {
            totalAntinodes++;
            totalAntinodesAfterUpdate++;
        }
    }
}

Console.WriteLine($"Total amount of antinodes: {totalAntinodes}");

foreach (var frequency in antennasByFrequency.Keys)
{
    var antennasCount = antennasByFrequency[frequency].Count;
    for (var i = 0; i < antennasCount; i++)
    for (var j = i + 1; j < antennasCount; j++)
    {
        var firstAntenna = antennasByFrequency[frequency][i];
        var secondAntenna = antennasByFrequency[frequency][j];

        var transition = new Transition(firstAntenna, secondAntenna);

        PutAntinodes(firstAntenna, transition);
        PutAntinodes(secondAntenna, transition.Reverse());
    }
}

Console.WriteLine($"Total amount of antinodes after update: {totalAntinodesAfterUpdate}");