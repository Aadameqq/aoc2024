const string dataFile = "../../../data/data.txt";

var pagesRestrictions = new Dictionary<int, List<int>>();
var sum = 0;
var correctedSum = 0;

int GetMiddleElement(int[] arr)
{
    var middle = arr.Length / 2;
    return arr[middle];
}

foreach (var line in File.ReadLines(dataFile))
    if (line.Contains('|'))
    {
        var split = line.Split('|').Select(int.Parse).ToArray();
        var (restricting, restricted) = (split[1], split[0]);

        if (pagesRestrictions.TryGetValue(restricting, out var found))
            found.Add(restricted);
        else
            pagesRestrictions.Add(split[1], [split[0]]);
    }
    else if (line.Contains(','))
    {
        var isCurrentOk = true;
        var cannotOccur = new HashSet<int>();
        var pages = line.Split(',').Select(int.Parse).ToArray();
        foreach (var page in pages)
        {
            if (cannotOccur.Contains(page))
            {
                isCurrentOk = false;
                break;
            }

            if (pagesRestrictions.TryGetValue(page, out var pagesToBlock))
                foreach (var pageToBlock in pagesToBlock)
                    cannotOccur.Add(pageToBlock);
        }

        if (isCurrentOk)
        {
            var middleElement = GetMiddleElement(pages);
            sum += middleElement;
        }
        else
        {
            correctPageSet(pages);
        }
    }


const int sameElement = 0;
const int shouldSwap = 1;
const int shouldNotSwap = -1;

void correctPageSet(int[] pages)
{
    Array.Sort(pages, (x, y) =>
    {
        if (x == y) return sameElement;

        if (pagesRestrictions.TryGetValue(x, out var pageRestrictions) && pageRestrictions.Contains(y))
            return shouldSwap;

        return shouldNotSwap;
    });

    correctedSum += GetMiddleElement(pages);
}


Console.WriteLine($"Sum : {sum}");
Console.WriteLine($"Sum of corrected : {correctedSum}");