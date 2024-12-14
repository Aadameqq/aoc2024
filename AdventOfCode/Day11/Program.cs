const string dataFile = "../../../data/data.txt";
var data = File.ReadAllLines(dataFile)[0].Split(" ");

var stones = new LinkedList<string>();

foreach (var line in data) stones.AddLast(line);

var blinks = 25;

for (var blinkIndex = 0; blinkIndex < blinks; blinkIndex++)
{
    var currentStone = stones.First;
    while (currentStone != null)
    {
        if (currentStone.Value == "0")
        {
            currentStone.Value = "1";
        }
        else if (currentStone.Value.Length % 2 == 0)
        {
            var middleIndex = currentStone.Value.Length / 2;
            var firstHalf = currentStone.Value[..middleIndex];
            var secondHalf = currentStone.Value[middleIndex..];
            var secondHalfWithoutLeadingZeros = secondHalf.TrimStart('0');
            currentStone.Value = secondHalfWithoutLeadingZeros == "" ? "0" : secondHalfWithoutLeadingZeros;
            stones.AddBefore(currentStone, firstHalf);
        }
        else
        {
            currentStone.Value = (long.Parse(currentStone.Value) * 2024).ToString();
        }

        currentStone = currentStone.Next;
    }
}

Console.WriteLine($"Stones: {stones.Count}");