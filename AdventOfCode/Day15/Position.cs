namespace Day15;

public record Position(int X, int Y)
{
    public Position Copy()
    {
        return new Position(X, Y);
    }
}