using Day9;
using File = System.IO.File;

const string dataFile = "./data/data.txt";

var diskMap = File.ReadAllLines(dataFile)[0].ToCharArray().Select(x => x - '0').ToArray();

var fileSystem = new FileSystem(diskMap);

foreach (var file in fileSystem.EnumerateFilesBackwards())
{
    fileSystem.DefragmentFile(file);
}

Console.WriteLine(fileSystem.CalculateCheckSum());

fileSystem = new FileSystem(diskMap);

foreach (var file in fileSystem.EnumerateFilesBackwards())
{
    foreach (var node in fileSystem.EnumerateFreeSpace())
    {
        var freeSpace = node.Value;
        if (file.Size() == 0 || file.DoesOccurBefore(freeSpace)) break;
        fileSystem.TryMoveFile(file, node);
    }
}

Console.WriteLine(fileSystem.CalculateCheckSum());