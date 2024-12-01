Console.WriteLine("hello");

const string dataFile = "../../../data/data.txt";
List<int> firstList = [];
List<int> secondList = [];


foreach (var line in File.ReadLines(dataFile))
{
    var locationIds = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    var first = int.Parse(locationIds[0]);
    var second = int.Parse(locationIds[1]);

    firstList.Add(first);
    secondList.Add(second);
}

firstList.Sort();
secondList.Sort();

var totalDistance = firstList.Zip(secondList, (first, second) => new { First = first, Second = second })
    .Sum(locations => Math.Abs(locations.First - locations.Second));

Console.WriteLine($"Total distance: {totalDistance}");

var secondIndex = 0;
var totalOccurrences = 0;

foreach (var location in firstList)
{
    var occurrences = 0;

    while (secondIndex < secondList.Count && secondList[secondIndex] <= location)
    {
        if (secondList[secondIndex] == location) occurrences++;
        secondIndex++;
    }


    totalOccurrences += occurrences * location;
}

Console.WriteLine($"Total occurrences: {totalOccurrences}");