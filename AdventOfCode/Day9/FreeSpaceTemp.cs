namespace Day9;

public record FreeSpaceTemp
{
    public FreeSpaceTemp(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }
    public int End { get; }

    public int Size => End - Start + 1;

    public FreeSpaceTemp TrimStart(int amount)
    {
        return new FreeSpaceTemp(Start + amount, End);
    }
}