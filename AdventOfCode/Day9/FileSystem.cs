using System.Collections;

namespace Day9;

public class FileSystem
{
    private readonly List<File> files = [];
    private readonly LinkedList<FreeSpace> freeSpaceList = [];
    private readonly Memory memory = new();

    public FileSystem(int[] diskMap)
    {
        var id = 0;
        for (var i = 0; i < diskMap.Length; i++)
        {
            if (IsDiskMapFragmentFreeSpace(i))
            {
                freeSpaceList.AddLast(new FreeSpace(memory.GetCapacity(), memory.GetCapacity() + diskMap[i] - 1));
                memory.AllocateFreeSpaceBlock(diskMap[i]);
                continue;
            }

            var indexes = memory.AllocateMemoryBlock(id, diskMap[i]);
            files.Add(new File(id, indexes));
            id++;
        }
    }

    private bool IsDiskMapFragmentFreeSpace(int index)
    {
        return index % 2 == 1;
    }

    public LinkedListNode<FreeSpace> GetFirstFreeSpace()
    {
        return freeSpaceList.First;
    }

    public IEnumerable<File> EnumerateFilesBackwards()
    {
        return files.AsEnumerable().Reverse();
    }

    public bool IsThereFreeSpace()
    {
        return freeSpaceList.Count != 0;
    }

    public void RemoveFile(File file)
    {
        memory.FreeMemory(file.Indexes);
        file.Indexes = [];
    }

    public void PartiallyRemoveFile(File file, int amount)
    {
        memory.FreeMemory(file.Indexes[^amount..]);
        file.Indexes = file.Indexes[..^amount];
    }

    public void OverwriteFreeSpace(File file, LinkedListNode<FreeSpace> node)
    {
        var freeSpace = node.Value;
        if (file.Size() > freeSpace.Size)
        {
            memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.End);
            PartiallyRemoveFile(file, freeSpace.Size);
            freeSpaceList.Remove(freeSpace);
            return;
        }

        memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.Start + file.Size() - 1);


        if (file.Size() == freeSpace.Size)
        {
            freeSpaceList.Remove(freeSpace);
            RemoveFile(file);
            return;
        }

        node.Value = freeSpace.TrimStart(file.Size());
        RemoveFile(file);
    }

    public IEnumerable<LinkedListNode<FreeSpace>> EnumerateFreeSpace()
    {
        return new FreeSpaceEnumerable(freeSpaceList);
    }

    public long CalculateCheckSum()
    {
        return memory.CalculateCheckSum();
    }

    private class FreeSpaceEnumerable(LinkedList<FreeSpace> freeSpaceList) : IEnumerable<LinkedListNode<FreeSpace>>
    {
        public IEnumerator<LinkedListNode<FreeSpace>> GetEnumerator()
        {
            for (var node = freeSpaceList.First; node != null; node = node.Next)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}