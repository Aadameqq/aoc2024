using System.Collections;

namespace Day9;

public class FreeSpace
{
    private readonly LinkedList<MemoryBlock> blocks = [];

    public void Add(MemoryBlock block)
    {
        blocks.AddLast(block);
    }

    public LinkedListNode<MemoryBlock> GetFirstFreeMemoryBlock()
    {
        return blocks.First;
    }

    public bool IsThereFreeSpace()
    {
        return blocks.Count != 0;
    }

    public IEnumerable<LinkedListNode<MemoryBlock>> EnumerateFreeSpace()
    {
        return new FreeSpaceEnumerable(blocks);
    }

    public void Remove(LinkedListNode<MemoryBlock> node)
    {
        blocks.Remove(node);
    }

    private class FreeSpaceEnumerable(LinkedList<MemoryBlock> blocks)
        : IEnumerable<LinkedListNode<MemoryBlock>>
    {
        public IEnumerator<LinkedListNode<MemoryBlock>> GetEnumerator()
        {
            for (var node = blocks.First; node != null; node = node.Next)
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