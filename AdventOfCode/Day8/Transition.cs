namespace Day8;

public class Transition(int x, int y)
{
    public Transition(Position p1, Position p2) : this(p1.x - p2.x, p1.y - p2.y)
    {
    }

    public Transition Reverse()
    {
        return new Transition(-1 * x, -1 * y);
    }

    public Position TransitionPosition(Position position)
    {
        return new Position(position.x + x, position.y + y);
    }
}