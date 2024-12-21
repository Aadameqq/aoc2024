using System.Text.RegularExpressions;
using Day13;

var totalCost = 0L;
var totalCostWithCorrectedCoordinates = 0L;

var costCalculator = new CostCalculator();
var withCorrectionCostCalculator = new CostCalculator(false);

var matches = GetRegexMatches();
foreach (Match match in matches)
{
    var values = match.Groups.Cast<Group>().Skip(1).Select(g => long.Parse(g.Value)).ToArray();
    var buttonA = new Position(values[0], values[1]);
    var buttonB = new Position(values[2], values[3]);
    var target = new Position(values[4], values[5]);

    totalCost += costCalculator.CalculateCost(buttonA, buttonB, target);
    totalCostWithCorrectedCoordinates +=
        withCorrectionCostCalculator.CalculateCost(buttonA, buttonB, CorrectTargetCoordinates(target));
}

Console.WriteLine($"Total cost: {totalCost}");
Console.WriteLine($"Total cost with corrected coordinates: {totalCostWithCorrectedCoordinates}");
return;

MatchCollection GetRegexMatches()
{
    const string dataFile = "../../../data/data.txt";
    var input = File.ReadAllText(dataFile);

    var pattern = @"Button A: X\+(\d+), Y\+(\d+)\s*" +
                  @"Button B: X\+(\d+), Y\+(\d+)\s*" +
                  @"Prize: X=(\d+), Y=(\d+)";

    return new Regex(pattern).Matches(input);
}

Position CorrectTargetCoordinates(Position incorrect)
{
    const long correctionFactor = 10_000_000_000_000;
    return new Position(incorrect.X + correctionFactor, incorrect.Y + correctionFactor);
}