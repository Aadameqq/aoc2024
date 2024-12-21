using System.Text.RegularExpressions;
using Day13;

var totalCost = 0L;
var totalCostWithCorrectedCoordinates = 0L;

Point correctTargetCoordinates(Point incorrect)
{
    const long correctionFactor = 10000000000000;
    return new Point(incorrect.X + correctionFactor, incorrect.Y + correctionFactor);
}

const string dataFile = "../../../data/data.txt";
var input = File.ReadAllText(dataFile);

var pattern = @"Button A: X\+(\d+), Y\+(\d+)\s*" +
              @"Button B: X\+(\d+), Y\+(\d+)\s*" +
              @"Prize: X=(\d+), Y=(\d+)";

var matches = new Regex(pattern).Matches(input);

foreach (Match match in matches)
{
    var buttonA = new Point(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
    var buttonB = new Point(long.Parse(match.Groups[3].Value), long.Parse(match.Groups[4].Value));
    var target = new Point(long.Parse(match.Groups[5].Value), long.Parse(match.Groups[6].Value));

    totalCost += CalculateCost(buttonA, buttonB, target);
    totalCostWithCorrectedCoordinates += CalculateCost(buttonA, buttonB, correctTargetCoordinates(target), false);
}

long CalculateDeterminant(long x, long y, long a, long b)
{
    return x * b - a * y;
}

bool HasSingleSolution(long mainDet, long xDet, long yDet)
{
    return mainDet != 0 && xDet % mainDet == 0 && yDet % mainDet == 0;
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