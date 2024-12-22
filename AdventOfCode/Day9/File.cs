namespace Day9;

public class File(int _id, MemoryBlock _memoryBlock)
{
    public readonly int Id = _id;
    private MemoryBlock memoryBlock = _memoryBlock;

    public int Size()
    {
        return memoryBlock.Size;
    }

    public void Empty()
    {
        memoryBlock = MemoryBlock.Empty;
    }

    public bool DoesOccurBefore(MemoryBlock otherMemoryBlock)
    {
        return memoryBlock.FirstIndex < otherMemoryBlock.FirstIndex;
    }

    public MemoryBlock ToMemoryBlock()
    {
        return memoryBlock;
    }

    public void ChangeMemoryBlock(MemoryBlock block)
    {
        memoryBlock = block;
    }
}