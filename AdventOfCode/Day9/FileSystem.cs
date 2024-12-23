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
        var newBlock = MemoryBlock.Empty;

        var size = file.Size();

        var node = freeSpace.GetFirstFreeMemoryBlock();

        while (node != null)
        {
            if (size <= 0) break;
            var freeBlock = node.Value;

            if (file.DoesOccurBefore(freeBlock)) break;

            var toMove = Math.Min(size, freeBlock.Size);

            var (firstFree, secondFree) = freeBlock.Split(toMove);

            newBlock = newBlock.JoinWith(firstFree);
            size -= firstFree.Size;
            if (secondFree.Size > 0)
            {
                node.Value = secondFree;
                node = node.Next;
            }
            else
            {
                node = node.Next;
                freeSpace.Remove(node.Previous);
            }
        }

        var oldBlock = file.ToMemoryBlock();

        if (size > 0)
        {
            var (left, _) = oldBlock.Split(size);
            newBlock = newBlock.JoinWith(left);
        }

        memory.FreeMemory(oldBlock);
        memory.WriteMemoryBlock(file.Id, newBlock);
        file.ChangeMemoryBlock(newBlock);
    }

    public IEnumerable<LinkedListNode<MemoryBlock>> EnumerateFreeSpace()
    {
        return freeSpace.EnumerateFreeSpace();
    }

    public long CalculateCheckSum()
    {
        return memory.CalculateCheckSum();
    }
}