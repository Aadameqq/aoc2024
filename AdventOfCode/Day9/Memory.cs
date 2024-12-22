using System.Collections;

namespace Day9;

public class Memory
{
    private const int FreeSpaceId = -1;
    private readonly List<File> files = [];
    private readonly List<FreeSpace> freeSpaceList = [];
    private readonly List<int> memory = [];

    public Memory(int[] diskMap)
    {
        var id = 0;
        for (var i = 0; i < diskMap.Length; i++)
        {
            if (IsDiskMapFragmentFreeSpace(i))
            {
                freeSpaceList.Add(new FreeSpace(memory.Count, memory.Count + diskMap[i] - 1));
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

    public FreeSpace GetFreeSpace(int index = 0)
    {
        return freeSpaceList[index];
    }

    public int GetFreeSpaceCount()
    {
        return freeSpaceList.Count;
    }

    public void RemoveFile(File file)
    {
        OverwriteMemorySet(FreeSpaceId, file.Start, file.End);
    }

    public void PartiallyRemoveFile(File file, int amount)
    {
        OverwriteMemorySet(FreeSpaceId, file.End - amount + 1, file.End);
    }

    public void OverwriteFreeSpace(FreeSpace freeSpace, File file, int freeSpaceId = -1)
    {
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

        if (freeSpaceId == -1)
        {
            freeSpaceId = freeSpaceList.IndexOf(freeSpace);
        }

        freeSpaceList[freeSpaceId] = freeSpace.TrimStart(file.Size());
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
}