namespace Day9;

public class Memory
{
    private const int FreeMemoryId = -1;
    private readonly List<int> memory = [];

    public MemoryBlock AllocateMemoryBlock(int id, int count)
    {
        List<int> indexes = [];
        for (var i = 0; i < count; i++)
        {
            indexes.Add(memory.Count);
            memory.Add(id);
        }

        return new MemoryBlock(indexes);
    }

    public MemoryBlock AllocateFreeSpaceBlock(int count)
    {
        return AllocateMemoryBlock(FreeMemoryId, count);
    }

    public void WriteMemoryBlock(int id, MemoryBlock block)
    {
        foreach (var index in block.Indexes)
        {
            if (memory[index] != FreeMemoryId)
            {
                throw new InvalidOperationException("Can only write to free memory");
            }

            memory[index] = id;
        }
    }

    public void FreeMemory(MemoryBlock block)
    {
        foreach (var index in block.Indexes)
        {
            memory[index] = FreeMemoryId;
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