namespace Day9;

public class File(int _id, List<int> _indexes)
{
    public readonly int Id = _id;
    public List<int> Indexes = [.._indexes];

    public int Size()
    {
        return Indexes.Count;
    }

    public int MinIndex()
    {
        return Indexes.Min();
    }
}