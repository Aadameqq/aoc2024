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

const int AVAILABLE_PRESSES_PER_BUTTON = 100;
const int B_PRESS_COST = 1;
const int A_PRESS_COST = 3;

bool HasSingleSolution(long mainDet, long xDet, long yDet)
{
    return mainDet != 0 && xDet % mainDet == 0 && yDet % mainDet == 0;
}

var totalCost = 0L;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line.Length == 0) continue;

    var firstButton = ParseButtonInput(line);
    var secondButton = ParseButtonInput(reader.ReadLine());
    var prize = ParsePrizeInput(reader.ReadLine());

    var mainDet = CalculateDeterminant(firstButton.X, firstButton.Y, secondButton.X, secondButton.Y);
    var xDet = CalculateDeterminant(prize.X, prize.Y, secondButton.X, secondButton.Y);
    var yDet = CalculateDeterminant(firstButton.X, firstButton.Y, prize.X, prize.Y);

    if (!HasSingleSolution(mainDet, xDet, yDet)) continue;

    var aPresses = xDet / mainDet;
    var bPresses = yDet / mainDet;

    if (aPresses <= AVAILABLE_PRESSES_PER_BUTTON && bPresses <= AVAILABLE_PRESSES_PER_BUTTON)
        totalCost += aPresses * A_PRESS_COST + bPresses * B_PRESS_COST;
}

Console.WriteLine($"Total cost: {totalCost}");