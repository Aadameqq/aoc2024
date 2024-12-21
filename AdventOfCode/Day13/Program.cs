using System.Text.RegularExpressions;
using Day13;

const string dataFile = "../../../data/data.txt";

var reader = new StreamReader(dataFile);

Point ParseButtonInput(string input)
{
    var match = Regex.Match(input, @"X\+(\d+), Y\+(\d+)");

    if (match.Success)
    {
        var x = long.Parse(match.Groups[1].Value);
        var y = long.Parse(match.Groups[2].Value);
        return new Point(x, y);
    }

    throw new ArgumentException($"Invalid input '{input}'");
}

Point ParsePrizeInput(string input)
{
    var match = Regex.Match(input, @"X\=(\d+), Y\=(\d+)");

    if (match.Success)
    {
        var x = long.Parse(match.Groups[1].Value);
        var y = long.Parse(match.Groups[2].Value);
        return new Point(x, y);
    }

    throw new ArgumentException($"Invalid input '{input}'");
}

long CalculateDeterminant(long x, long y, long a, long b)
{
    return x * b - a * y;
}


bool HasSingleSolution(long mainDet, long xDet, long yDet)
{
    return mainDet != 0 && xDet % mainDet == 0 && yDet % mainDet == 0;
}

Point correctPrizeCoordinates(Point incorrect)
{
    const long correctionFactor = 10000000000000;
    return new Point(incorrect.X + correctionFactor, incorrect.Y + correctionFactor);
}

var totalCost = 0L;
var totalCostWithCorrectedCoordinates = 0L;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line.Length == 0) continue;

    var firstButton = ParseButtonInput(line);
    var secondButton = ParseButtonInput(reader.ReadLine());
    var prize = ParsePrizeInput(reader.ReadLine());

    totalCost += CalculateCost(firstButton, secondButton, prize);
    totalCostWithCorrectedCoordinates +=
        CalculateCost(firstButton, secondButton, correctPrizeCoordinates(prize), false);
}

long CalculateCost(Point buttonA, Point buttonB, Point target, bool shouldRestrictPresses = true)
{
    const int availablePressesPerButton = 100;
    const int bPressCost = 1;
    const int aPressCost = 3;

    var mainDet = CalculateDeterminant(buttonA.X, buttonA.Y, buttonB.X, buttonB.Y);
    var xDet = CalculateDeterminant(target.X, target.Y, buttonB.X, buttonB.Y);
    var yDet = CalculateDeterminant(buttonA.X, buttonA.Y, target.X, target.Y);

    if (!HasSingleSolution(mainDet, xDet, yDet)) return 0;

    var aPresses = xDet / mainDet;
    var bPresses = yDet / mainDet;

    if (shouldRestrictPresses &&
        (aPresses > availablePressesPerButton || bPresses > availablePressesPerButton)) return 0;

    return aPresses * aPressCost + bPresses * bPressCost;
}

Console.WriteLine($"Total cost: {totalCost}");
Console.WriteLine($"Total cost with corrected coordinates: {totalCostWithCorrectedCoordinates}");