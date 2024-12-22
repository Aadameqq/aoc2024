using Day9;
using File = System.IO.File;

const string dataFile = "../../../data/data.txt";

var diskMap = File.ReadAllLines(dataFile)[0].ToCharArray().Select(x => x - '0').ToArray();

var fileSystem = new FileSystem(diskMap);

// foreach (var file in memory.EnumerateFilesBackwards())
// {
//     while (file.Size() > 0 && file.MinIndex() > memory.GetFirstFreeSpace().Value.Start)
//     {
//         if (!memory.IsThereFreeSpace())
//         {
//             break;
//         }
//
//         var freeSpace = memory.GetFirstFreeSpace();
//         memory.OverwriteFreeSpace(file, freeSpace);
//     }
// }
//
// Console.WriteLine(memory.CalculateCheckSum());
// Console.WriteLine(memory.CalculateCheckSum() == 6385338159127);

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
Console.WriteLine(fileSystem.CalculateCheckSum() == 6415163624282);