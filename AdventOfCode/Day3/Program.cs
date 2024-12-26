using System.Text.RegularExpressions;

const string dataFile = "./data/data.txt";

var sum = 0;
var sumWithConditionals = 0;

var memory = File.ReadAllText(dataFile);

var matches = Regex.Matches(memory, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");

var isEnabled = true;

foreach (var match in matches.ToArray())
{
    var operation = match.Groups[0].Value;

    if (operation.StartsWith("do"))
    {
        isEnabled = !operation.StartsWith("don't");
    }
    else
    {
        var firstNum = int.Parse(match.Groups[1].Value);
        var secondNum = int.Parse(match.Groups[2].Value);
        var multiplication = firstNum * secondNum;

        sum += multiplication;

        if (isEnabled) sumWithConditionals += multiplication;
    }
}

Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Sum with conditionals: {sumWithConditionals}");