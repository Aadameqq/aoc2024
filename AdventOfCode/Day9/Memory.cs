using System.Collections;

namespace Day9;

public class Memory
{
    private const int FreeSpaceId = -1;
    private readonly List<File> files = [];
    private readonly LinkedList<FreeSpace> freeSpaceList = [];
    private readonly List<int> memory = [];

    public Memory(int[] diskMap)
    {
        var id = 0;
        for (var i = 0; i < diskMap.Length; i++)
        {
            if (IsDiskMapFragmentFreeSpace(i))
            {
                freeSpaceList.AddLast(new FreeSpace(memory.Count, memory.Count + diskMap[i] - 1));
                AddMemorySet(FreeSpaceId, diskMap[i]);
                continue;
            }

            files.Add(new File(id, memory.Count, memory.Count + diskMap[i] - 1));
            AddMemorySet(id, diskMap[i]);
            id++;
        }
    }

    private bool IsDiskMapFragmentFreeSpace(int index)
    {
        return index % 2 == 1;
    }

    private void AddMemorySet(int id, int count)
    {
        for (var i = 0; i < count; i++)
        {
            memory.Add(id);
        }
    }

    public LinkedListNode<FreeSpace> GetFirstFreeSpace()
    {
        return freeSpaceList.First;
    }

    private void OverwriteMemorySet(int id, int start, int end)
    {
        for (var i = start; i <= end; i++)
        {
            memory[i] = id;
        }
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
        OverwriteMemorySet(FreeSpaceId, file.Start, file.End);
    }

    public void PartiallyRemoveFile(File file, int amount)
    {
        OverwriteMemorySet(FreeSpaceId, file.End - amount + 1, file.End);
    }

    public void OverwriteFreeSpace(File file, LinkedListNode<FreeSpace> node)
    {
        var freeSpace = node.Value;
        if (file.Size() > freeSpace.Size)
        {
            OverwriteMemorySet(file.Id, freeSpace.Start, freeSpace.End);
            PartiallyRemoveFile(file, freeSpace.Size);
            file.End -= freeSpace.Size;
            freeSpaceList.Remove(freeSpace);
            return;
        }

        OverwriteMemorySet(file.Id, freeSpace.Start, freeSpace.Start + file.Size() - 1);
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

    public long CalculateCheckSum()
    {
        var checkSum = 0L;
        for (var i = 0; i < memory.Count; i++)
        {
            if (memory[i] == FreeSpaceId) continue;
            checkSum += memory[i] * i;
        }

        return checkSum;
    }

    public IEnumerable<LinkedListNode<FreeSpace>> EnumerateFreeSpace()
    {
        return new FreeSpaceEnumerable(freeSpaceList);
    }

    public class FilesEnumerable(List<File> files) : IEnumerable<File>
    {
        public IEnumerator<File> GetEnumerator()
        {
            for (var i = files.Count - 1; i >= 0; i--)
            {
                yield return files[i];
            }
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