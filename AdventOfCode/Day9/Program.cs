using Day9;
using File = System.IO.File;

const string dataFile = "../../../data/data.txt";

var diskMap = File.ReadAllLines(dataFile)[0].ToCharArray().Select(x => x - '0').ToArray();

var memory = new Memory(diskMap);

foreach (var file in memory.EnumerateFilesBackwards())
{
    var good = true;
    while (file.Size() > 0 && file.Start > memory.GetFreeSpace().Start)
    {
        if (!memory.IsThereFreeSpace())
        {
            break;
        }

        var freeSpace = memory.GetFreeSpace();
        memory.OverwriteFreeSpace(freeSpace, file);
    }

    if (!good) break;
}

Console.WriteLine(memory.CalculateCheckSum());

memory = new Memory(diskMap);

foreach (var file in memory.EnumerateFilesBackwards())
{
    Console.WriteLine(file.Start);
    for (var i = 0; i < memory.GetFreeSpaceCount(); i++)
    {
        var freeSpace = memory.GetFreeSpace(i);
        if (freeSpace.Start > file.Start) break;
        if (freeSpace.Size < file.Size()) continue;
        memory.OverwriteFreeSpace(freeSpace, file, i);
    }
}

Console.WriteLine(memory.CalculateCheckSum());