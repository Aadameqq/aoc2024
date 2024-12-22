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
                memory.AddFreeSpaceBlock(diskMap[i]);
                continue;
            }

            files.Add(new File(id, memory.GetCapacity(), memory.GetCapacity() + diskMap[i] - 1));
            memory.AddMemoryBlock(id, diskMap[i]);
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

    public FilesEnumerable EnumerateFilesBackwards()
    {
        return new FilesEnumerable(files);
    }

    public bool IsThereFreeSpace()
    {
        return freeSpaceList.Count != 0;
    }

    public void RemoveFile(File file)
    {
        memory.FreeMemory(file.Start, file.End);
    }

    public void PartiallyRemoveFile(File file, int amount)
    {
        memory.FreeMemory(file.End - amount + 1, file.End);
    }

    public void OverwriteFreeSpace(File file, LinkedListNode<FreeSpace> node)
    {
        var freeSpace = node.Value;
        if (file.Size() > freeSpace.Size)
        {
            memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.End);
            PartiallyRemoveFile(file, freeSpace.Size);
            file.End -= freeSpace.Size;
            freeSpaceList.Remove(freeSpace);
            return;
        }

        memory.WriteMemoryBlock(file.Id, freeSpace.Start, freeSpace.Start + file.Size() - 1);
        RemoveFile(file);

        if (file.Size() == freeSpace.Size)
        {
            freeSpaceList.Remove(freeSpace);
            file.Start = file.End + 1;
            return;
        }


        node.Value = freeSpace.TrimStart(file.Size());
        file.Start = file.End + 1;
    }


    public IEnumerable<LinkedListNode<FreeSpace>> EnumerateFreeSpace()
    {
        return new FreeSpaceEnumerable(freeSpaceList);
    }

    public long CalculateCheckSum()
    {
        return memory.CalculateCheckSum();
    }

    public class FilesEnumerable(List<File> files) : IEnumerable<File>
    {
        public IEnumerator<File> GetEnumerator()
        {
            return files.AsEnumerable().Reverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FreeSpaceEnumerable(LinkedList<FreeSpace> freeSpaceList) : IEnumerable<LinkedListNode<FreeSpace>>
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