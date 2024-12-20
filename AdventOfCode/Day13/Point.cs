namespace Day13;

public record Point(int X, int Y)
{
    public bool IsPositive()
    {
        return X >= 0 && Y >= 0;
    }

    public static Point operator -(Point first, Point second)
    {
        return new Point(first.X - second.X, first.Y - second.Y);
    }

    public bool IsScaledBy(Point other)
    {
        return X % other.X == 0 && Y % other.Y == 0 && X / other.X == Y / other.Y;
    }

    public int GetScaleFactor(Point other)
    {
        // TODO: validation
        return X / other.X;
    }

    public Point MultiplyBy(int scalar)
    {
        return new Point(X * scalar, Y * scalar);
    }
}