const string dataFile = "../../../data/data.txt";


bool CheckPath(long numToAchieve, int[] nums, long acc = 0, int index = 0)
{
    if (index == nums.Length && numToAchieve == acc) return true;
    if (index == nums.Length || acc > numToAchieve) return false;

    var addition = CheckPath(numToAchieve, nums, acc + nums[index], index + 1);
    var multiplication =
        index != 0 && CheckPath(numToAchieve, nums, acc * nums[index], index + 1);

    return addition || multiplication;
}

long Concat(long x, long y)
{
    var result = $"{x}{y}";
    return long.Parse(result);
}

bool CheckPathWithConcat(long numToAchieve, int[] nums, long acc = 0, int index = 0)
{
    if (index == nums.Length && numToAchieve == acc) return true;
    if (index == nums.Length || acc > numToAchieve) return false;

    var addition = CheckPathWithConcat(numToAchieve, nums, acc + nums[index], index + 1);
    var multiplication =
        index != 0 && CheckPathWithConcat(numToAchieve, nums, acc * nums[index], index + 1);
    var concatenation =
        index != 0 && CheckPathWithConcat(numToAchieve, nums, Concat(acc, nums[index]), index + 1);

    return addition || multiplication || concatenation;
}

var total = 0L;
var totalWithConcat = 0L;

foreach (var line in File.ReadLines(dataFile))
{
    var split = line.Split(":");
    var (numToAchieve, nums) = (long.Parse(split[0]),
        split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

    if (CheckPath(numToAchieve, nums)) total += numToAchieve;
    if (CheckPathWithConcat(numToAchieve, nums)) totalWithConcat += numToAchieve;
}

Console.WriteLine($"Sum: {total}");
Console.WriteLine($"Sum with concat {totalWithConcat}");