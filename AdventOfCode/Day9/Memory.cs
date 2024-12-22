namespace Day9;

public class Memory
{
    private const int FreeMemoryId = -1;
    private readonly List<int> memory = [];

    public void AddMemoryBlock(int id, int count)
    {
        for (var i = 0; i < count; i++)
        {
            memory.Add(id);
        }
    }

    public void AddFreeSpaceBlock(int count)
    {
        for (var i = 0; i < count; i++)
        {
            memory.Add(FreeMemoryId);
        }
    }

    public void WriteMemoryBlock(int id, int start, int end)
    {
        for (var i = start; i <= end; i++)
        {
            if (memory[i] != FreeMemoryId)
            {
                throw new InvalidOperationException("Can only write to free memory");
            }

            memory[i] = id;
        }
    }

    public void FreeMemory(int start, int end)
    {
        for (var i = start; i <= end; i++)
        {
            memory[i] = FreeMemoryId;
        }
    }

    public int GetCapacity()
    {
        return memory.Count;
    }

    public long CalculateCheckSum()
    {
        return memory.Select((id, index) => id == FreeMemoryId ? 0 : (long)(id * index)).Sum();
    }
}