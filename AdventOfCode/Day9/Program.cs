const string dataFile = "../../../data/data.txt";

var diskMap = File.ReadAllLines(dataFile)[0].ToCharArray().Select(x => x - '0').ToArray();

var indexedDiskMap = new List<int>();
var freeSpace = new List<int>();

for (var i = 0; i < diskMap.Length; i += 2) indexedDiskMap.Add(diskMap[i]);
for (var i = 1; i < diskMap.Length; i += 2) freeSpace.Add(diskMap[i]);

var compressedDisk = new List<int>();

var currentlyCompressedIndex = indexedDiskMap.Count - 1;

for (var i = 0; i < freeSpace.Count; i++)
{
    for (var j = 0; j < indexedDiskMap[i]; j++) compressedDisk.Add(i);

    for (var j = 0; j < freeSpace[i] && i < currentlyCompressedIndex; j++)
    {
        compressedDisk.Add(currentlyCompressedIndex);
        indexedDiskMap[currentlyCompressedIndex]--;
        if (indexedDiskMap[currentlyCompressedIndex] == 0) currentlyCompressedIndex--;
    }
}

var checkSum = 0L;
var index = 0;
foreach (var filePart in compressedDisk)
{
    checkSum += filePart * index;
    index++;
}

Console.WriteLine($"Checksum: {checkSum}");