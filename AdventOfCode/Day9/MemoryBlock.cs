namespace Day9;

public record MemoryBlock(List<int> Indexes)
{
    public static MemoryBlock Empty => new([]);

    public int Size => Indexes.Count;

    public int FirstIndex => Size == 0 ? -1 : Indexes.Min();

    public (MemoryBlock, MemoryBlock) Split(int index)
    {
        var first = new MemoryBlock(Indexes[..index]);
        var second = new MemoryBlock(Indexes[index..]);

        return (first, second);
    }
}