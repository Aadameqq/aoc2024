namespace Day9;

public class File(int _id, int _start, int _end)
{
    public readonly int Id = _id;
    public int End = _end;
    public int Start = _start;

    public int Size()
    {
        return End - Start + 1;
    }
}