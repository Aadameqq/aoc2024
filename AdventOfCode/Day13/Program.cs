using System.Text.RegularExpressions;
using Day13;

const string dataFile = "../../../data/data.txt";

var reader = new StreamReader(dataFile);

Point ParseButtonInput(string input)
{
    var match = Regex.Match(input, @"X\+(\d+), Y\+(\d+)");

    if (match.Success)
    {
        var x = int.Parse(match.Groups[1].Value);
        var y = int.Parse(match.Groups[2].Value);
        return new Point(x, y);
    }

    throw new ArgumentException($"Invalid input '{input}'");
}

Point ParsePrizeInput(string input)
{
    var match = Regex.Match(input, @"X\=(\d+), Y\=(\d+)");

    if (match.Success)
    {
        var x = int.Parse(match.Groups[1].Value);
        var y = int.Parse(match.Groups[2].Value);
        return new Point(x, y);
    }

    throw new ArgumentException($"Invalid input '{input}'");
}

var totalCost = 0;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line.Length == 0) continue;

    var firstButton = ParseButtonInput(line);
    var secondButton = ParseButtonInput(reader.ReadLine());
    var prize = ParsePrizeInput(reader.ReadLine());

    var cost = 0;

    var firstButtonPresses = 0;

    while (prize.IsPositive() && firstButtonPresses <= 100)
    {
        if (prize.IsScaledBy(secondButton))
        {
            var scale = prize.GetScaleFactor(secondButton);

            if (scale <= 100)
            {
                cost += scale;
                prize -= secondButton.MultiplyBy(scale);
                break;
            }
        }

        prize -= firstButton;
        cost += 3;
        firstButtonPresses++;
    }

    if (prize == new Point(0, 0) && firstButtonPresses <= 100) totalCost += cost;
}

Console.WriteLine($"Total cost: {totalCost}");
// 38714