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

var totalCost = 0L;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line.Length == 0) continue;

    var firstButton = ParseButtonInput(line);
    var secondButton = ParseButtonInput(reader.ReadLine());
    var prize = ParsePrizeInput(reader.ReadLine());

    var top = prize.Y * firstButton.X - prize.X * firstButton.Y;
    var bottom = secondButton.Y * firstButton.X - secondButton.X * firstButton.Y;
    if (top % bottom != 0) continue;
    var b = top / bottom;

    var a = (prize.X - b * secondButton.X) / firstButton.X;

    if (a <= 100 && b <= 100 && a >= 0 && b >= 0) totalCost += a * 3 + b;
}

Console.WriteLine($"Total cost: {totalCost}");
reader = new StreamReader(dataFile);
var totalCost2 = 0L;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line.Length == 0) continue;

    var firstButton = ParseButtonInput(line);
    var secondButton = ParseButtonInput(reader.ReadLine());
    var prize = ParsePrizeInput(reader.ReadLine());

    prize += new Point(10000000000000, 10000000000000);

    var top = prize.Y * firstButton.X - prize.X * firstButton.Y;
    var bottom = secondButton.Y * firstButton.X - secondButton.X * firstButton.Y;
    if (top % bottom != 0 || bottom == 0) continue;
    var b = top / bottom;

    top = prize.X - b * secondButton.X;
    bottom = firstButton.X;
    if (top % bottom != 0 || bottom == 0) continue;
    var a = top / bottom;


    if (a >= 0 && b >= 0) totalCost2 += a * 3 + b;
}

Console.WriteLine($"Total cost: {totalCost2}");