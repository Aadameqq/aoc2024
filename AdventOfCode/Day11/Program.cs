using Day11;

const string dataFile = "./data/data.txt";
var input = File.ReadAllLines(dataFile)[0].Split(" ");

var stones = new Stones(input);

void ApplyBlinks(int blinksAmount)
{
    for (var blinkIndex = 0; blinkIndex < blinksAmount; blinkIndex++)
    {
        var changes = stones.DumpForChanges();
        foreach (var stone in stones)
        {
            var stoneCount = stones.GetStoneCount(stone);
            if (stone == "0")
            {
                changes.IncreaseStoneCount("1", stoneCount);
            }
            else if (stone.Length % 2 == 0)
            {
                var middleIndex = stone.Length / 2;

                var firstHalf = stone[..middleIndex];
                var secondHalf = stone[middleIndex..];

                var secondHalfWithoutLeadingZeros = secondHalf.TrimStart('0') == "" ? "0" : secondHalf.TrimStart('0');

                changes.IncreaseStoneCount(firstHalf, stoneCount);
                changes.IncreaseStoneCount(secondHalfWithoutLeadingZeros, stoneCount);
            }
            else
            {
                changes.IncreaseStoneCount((long.Parse(stone) * 2024).ToString(), stoneCount);
            }

            changes.DecreaseStoneCount(stone, stoneCount);
        }

        stones.ApplyChanges(changes);
    }
}


var partOneBlinksAmount = 25;
ApplyBlinks(partOneBlinksAmount);
Console.WriteLine($"Stones after 25 blinks: {stones.Count()}");

var partTwoBlinksAmount = 75;
ApplyBlinks(partTwoBlinksAmount - partOneBlinksAmount);
Console.WriteLine($"Stones after 75 blinks: {stones.Count()}");