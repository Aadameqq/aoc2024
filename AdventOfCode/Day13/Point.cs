namespace Day13;

public record Point(long X, long Y)
{
    public static Point operator -(Point first, Point second)
    {
        return new Point(first.X - second.X, first.Y - second.Y);
    }

    public static Point operator +(Point first, Point second)
    {
        return new Point(first.X + second.X, first.Y + second.Y);
    }
}