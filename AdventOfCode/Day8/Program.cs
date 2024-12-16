using Day8;

const string dataFile = "../../../data/data.txt";

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

var totalAntinodes = 0;

bool IsWithinBounds(Position position)
{
    return position.x >= 0 && position.x < sizeX && position.y >= 0 && position.y < sizeY;
}

void TryAddAntinode(Position antinode)
{
    if (IsWithinBounds(antinode) && !antinodes[antinode.y][antinode.x])
    {
        antinodes[antinode.y][antinode.x] = true;
        totalAntinodes++;
    }
}

void PutAntinodes(Position position, Translation translation)
{
    var antinodePosition = position;
    while (IsWithinBounds(antinodePosition))
    {
        TryAddAntinode(antinodePosition);
        antinodePosition = translation.TranslatePosition(antinodePosition);
    }
}

foreach (var frequency in antennasByFrequency.Keys)
{
    var antennasCount = antennasByFrequency[frequency].Count;
    for (var i = 0; i < antennasCount; i++)
    for (var j = i + 1; j < antennasCount; j++)
    {
        var firstAntenna = antennasByFrequency[frequency][i];
        var secondAntenna = antennasByFrequency[frequency][j];

        var translation = new Translation(firstAntenna, secondAntenna);

        var firstAntinode = translation.TranslatePosition(firstAntenna);
        var secondAntinode = translation.Reverse().TranslatePosition(secondAntenna);

        TryAddAntinode(firstAntinode);
        TryAddAntinode(secondAntinode);
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

        var translation = new Translation(firstAntenna, secondAntenna);

        PutAntinodes(firstAntenna, translation);
        PutAntinodes(secondAntenna, translation.Reverse());
    }
}

Console.WriteLine($"Total amount of antinodes after update: {totalAntinodes}");