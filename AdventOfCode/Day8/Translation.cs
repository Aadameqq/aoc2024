namespace Day8;

public class Translation(int x, int y)
{
    public Translation(Position p1, Position p2) : this(p1.x - p2.x, p1.y - p2.y)
    {
    }

    public Translation Reverse()
    {
        return new Translation(-1 * x, -1 * y);
    }

    public Position TranslatePosition(Position position)
    {
        return new Position(position.x + x, position.y + y);
    }
}