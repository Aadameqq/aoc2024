namespace Day9;

public record FreeSpace
{
    public FreeSpace(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }
    public int End { get; }

    public int Size => End - Start + 1;

    public FreeSpace TrimStart(int amount)
    {
        return new FreeSpace(Start + amount, End);
    }
}