using Day9;
using File = System.IO.File;

const string dataFile = "../../../data/data.txt";

var diskMap = File.ReadAllLines(dataFile)[0].ToCharArray().Select(x => x - '0').ToArray();

var memory = new FileSystem(diskMap);

foreach (var file in memory.EnumerateFilesBackwards())
{
    while (file.Size() > 0 && file.MinIndex() > memory.GetFirstFreeSpace().Value.Start)
    {
        if (!memory.IsThereFreeSpace())
        {
            break;
        }

        var freeSpace = memory.GetFirstFreeSpace();
        memory.OverwriteFreeSpace(file, freeSpace);
    }
}

Console.WriteLine(memory.CalculateCheckSum());
Console.WriteLine(memory.CalculateCheckSum() == 6385338159127);

memory = new FileSystem(diskMap);

foreach (var file in memory.EnumerateFilesBackwards())
{
    foreach (var node in memory.EnumerateFreeSpace())
    {
        var freeSpace = node.Value;
        if (file.Indexes.Count == 0 || freeSpace.Start > file.MinIndex()) break;
        if (freeSpace.Size < file.Size()) continue;
        memory.OverwriteFreeSpace(file, node);
    }
}

Console.WriteLine(memory.CalculateCheckSum());
Console.WriteLine(memory.CalculateCheckSum() == 6415163624282);