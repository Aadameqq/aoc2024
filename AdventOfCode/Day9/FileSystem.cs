namespace Day9;

public class FileSystem
{
    private readonly List<File> files = [];
    private readonly FreeSpace freeSpace = new();
    private readonly Memory memory = new();

    public FileSystem(int[] diskMap)
    {
        var id = 0;
        for (var i = 0; i < diskMap.Length; i++)
        {
            if (IsDiskMapFragmentFreeSpace(i))
            {
                var memoryBlock = memory.AllocateFreeSpaceBlock(diskMap[i]);
                freeSpace.Add(memoryBlock);
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

    public IEnumerable<File> EnumerateFilesBackwards()
    {
        return files.AsEnumerable().Reverse();
    }


    // public void PartiallyRemoveFile(File file, int amount)
    // {
    //     memory.FreeMemory(file.Indexes[^amount..]);
    //     file.Indexes = file.Indexes[..^amount];
    // }

    public bool TryMoveFile(File file, LinkedListNode<MemoryBlock> node)
    {
        var freeSpaceBlock = node.Value;

        if (freeSpaceBlock.Size < file.Size())
        {
            return false;
        }

        var (firstBlock, secondBlock) = freeSpaceBlock.Split(file.Size());

        memory.WriteMemoryBlock(file.Id, firstBlock);
        memory.FreeMemory(file.ToMemoryBlock());
        file.ChangeMemoryBlock(firstBlock);
        if (secondBlock.Size > 0)
        {
            node.Value = secondBlock;
        }
        else
        {
            freeSpace.Remove(node);
        }

        return true;
    }

    public void DefragmentFile(File file)
    {
    }

    // public void OverwriteFreeSpace(File file, LinkedListNode<FreeSpaceTemp> node)
    // {
    //     var freeSpace = node.Value;
    //     if (file.Size() > freeSpace.Size)
    //     {
    //         memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.End);
    //         PartiallyRemoveFile(file, freeSpace.Size);
    //         freeSpaceList.Remove(freeSpace);
    //         return;
    //     }
    //
    //     memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.Start + file.Size() - 1);
    //
    //
    //     if (file.Size() == freeSpace.Size)
    //     {
    //         freeSpaceList.Remove(freeSpace);
    //         RemoveFile(file);
    //         return;
    //     }
    //
    //     node.Value = freeSpace.TrimStart(file.Size());
    //     RemoveFile(file);
    // }

    public IEnumerable<LinkedListNode<MemoryBlock>> EnumerateFreeSpace()
    {
        return freeSpace.EnumerateFreeSpace();
    }

    public long CalculateCheckSum()
    {
        return memory.CalculateCheckSum();
    }
}